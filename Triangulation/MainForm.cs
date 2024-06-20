using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


// ReSharper disable once CheckNamespace
namespace Triangulation;
public partial class AppForm : Form
{
    public AppForm(IDomainLogic domainLogic)
    {
        InitializeComponent();
        _upperLeftPointOfFragment = new Point(0, 0);
        _lowerRightPointOfFragment = new Point(255, 255);
        _domainLogic = domainLogic;
    }

    private readonly IDomainLogic _domainLogic;

    private Image _currentImage;
    public static Image Mem;
    private Point _upperLeftPointOfFragment;
    private Point _lowerRightPointOfFragment;
    private Bitmap _fragment;

    private void Open_Click(object sender, EventArgs e)
    {
        cleanButton.PerformClick();
        var imageFileDialog = new OpenFileDialog
        {
            Filter = @"Image files|*.jpeg; *.jpg; *.bmp; *.png; *.gif",
            RestoreDirectory = true
        };
        if (imageFileDialog.ShowDialog() != DialogResult.OK) return;
        try
        {
            var str = imageFileDialog.OpenFile();
            using (str)
            {
                var sizeOfWorkArea = new Size(256, 256);
                var bm = new Bitmap(str);
                var tm = new Bitmap(bm, sizeOfWorkArea);
                originalPicture.Image = tm;
                _currentImage = tm;
                Mem = tm;
            }
        }
        catch (IOException ex)
        {
            MessageBox.Show($@"Error: Could not read file from disk. Original error: {ex.Message}");
        }
    }

    public PictureBox GetSecondPb()
    {
        return poligonalNet;
    }

    public static List<Vertex> Vertexes = new List<Vertex>();
    private List<Triangle> _triangles = new List<Triangle>();
    public static readonly Bitmap BmpLines = new Bitmap(256, 256);
    private static readonly Graphics Graphics = Graphics.FromImage(BmpLines);

    private static void InitBitmap()
    {
        for (var i = 0; i < 256; i++)
        {
            for (var j = 0; j < 256; j++)
            {
                BmpLines.SetPixel(i, j, Color.White);
            }
        }
    }

    public static void MarkRed(int x, int y)
    {
        var bmp = (Bitmap)Mem;
        BmpLines.SetPixel(x, y, Color.Red);
        var curr = bmp.GetPixel(x, y);
        Vertexes.Add(new Vertex(x, y, curr));
    }

    private static List<Vertex> DeleteVertexes(List<Vertex> vertexes)
    {
        for (var i = 3; (i < vertexes.Count - 3); i++)
        {
            for (var j = 0; (j < vertexes.Count); j++)
            {
                if ((Math.Abs(vertexes[i].X - vertexes[j].X) < 2) && (Math.Abs(vertexes[i].Y - vertexes[j].Y) < 2))
                {
                    vertexes.RemoveAt(j);
                }
            }
        }
        return vertexes;
    }

    private void DivideImage_Click(object sender, EventArgs e)
    {
        try
        {
            var rootRect = new Rectangle(this, 0, 0, 256, 256, true, null);
            InitBitmap();
            var brightness = (int)numericUpDown1.Value;

            if (rootRect.CheckBrightness(0, 0, 256, 256, brightness))
            {
                rootRect.DivideImage(brightness, rootRect);
            }

            Vertexes.Add(new(0, 0, ((Bitmap)Mem).GetPixel(0, 0)));
            Vertexes.Add(new(poligonalNet.Size.Width - 1, 0, ((Bitmap)Mem).GetPixel(poligonalNet.Size.Width - 1, 0)));
            Vertexes.Add(new(0, poligonalNet.Size.Height - 1, ((Bitmap)Mem).GetPixel(0, poligonalNet.Size.Height - 1)));
            Vertexes.Add(new(poligonalNet.Size.Width - 1, poligonalNet.Size.Height - 1, ((Bitmap)Mem).GetPixel(poligonalNet.Size.Width - 1, poligonalNet.Size.Height - 1)));

            foreach (var v in Vertexes)
            {
                BmpLines.SetPixel(v.X, v.Y, v.CurrentColor);
            }

            poligonalNet.Image = BmpLines;
            poligonalNet.Refresh();
            Vertexes = DeleteVertexes(Vertexes);
            GC.Collect();
            ShowTops();
        }
        catch (NullReferenceException)
        {
            MessageBox.Show(@"Ошибка: файл не открыт");
        }
    }

    private void ShowTops()
    {
        var temp = new Bitmap(256, 256);
        try
        {
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 256; j++)
                {
                    temp.SetPixel(i, j, Color.White);
                }
            }

            foreach (var t in Vertexes)
            {
                temp.SetPixel(t.X, t.Y, t.CurrentColor);
            }
            triangulationPoints.Image = temp;
        }
        catch (NullReferenceException)
        {
            MessageBox.Show(@"Что-то тут явно не так");
        }
    }

    private static void DrawSide(Vertex begin, Vertex end)
    {
        Graphics.DrawLine(new Pen(Color.Black), new Point(begin.X, begin.Y), new Point(end.X, end.Y));
    }

    private static int GetVertexDir(Vertex first, Vertex second, Vertex third)
    {
        var turn = (third.X - first.X) * (second.Y - first.Y) - (third.Y - first.Y) * (second.X - first.X);
        if (turn > 0)
        {
            return 1; //r
        }
        if (turn < 0)
        {
            return -1; //l
        }
        return 0;
    }

    private void Triangulate(Queue<int> qeVertexes1, Queue<int> qeVertexes2, Queue<int> qeDirection, List<HashSet<int>> matrix)
    {
        var f = qeVertexes1.Dequeue();
        var s = qeVertexes2.Dequeue();
        var first = Vertexes[f];
        var second = Vertexes[s];
        var dir = qeDirection.Dequeue();
        DrawSide(first, second);
        var checkVert = false;
        double min = 2;
        var n = 0;
        for (var m = 0; m < Vertexes.Count; m++)
        {
            if ((dir != 0 && GetVertexDir(first, second, Vertexes[m]) == dir))
            {
                continue;
            }

            if (matrix[f].Contains(m) && matrix[s].Contains(m))
            {
                return;
            }

            var fs = Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2);
            var fm = Math.Pow(first.X - Vertexes[m].X, 2) + Math.Pow(first.Y - Vertexes[m].Y, 2);
            var ms = Math.Pow(Vertexes[m].X - second.X, 2) + Math.Pow(Vertexes[m].Y - second.Y, 2);
            var curr = (-fs + fm + ms) / (2 * Math.Sqrt(fm * ms));
            if (!(curr < min)) continue;
            min = curr;
            checkVert = true;
            n = m;
        }

        if (!checkVert) return;
        matrix[f].Add(n);
        matrix[s].Add(n);
        matrix[n].Add(f);
        matrix[n].Add(s);
        DrawSide(first, Vertexes[n]);
        DrawSide(second, Vertexes[n]);
        qeVertexes1.Enqueue(f);
        qeVertexes2.Enqueue(n);
        qeVertexes1.Enqueue(n);
        qeVertexes2.Enqueue(s);
        qeDirection.Enqueue(-GetVertexDir(first, second, Vertexes[n]));
        qeDirection.Enqueue(-GetVertexDir(first, second, Vertexes[n]));
    }

    private static List<Triangle> GetTriangles(IReadOnlyList<HashSet<int>> matrix)
    {

        var triangles = new List<Triangle>();

        for (var i = 0; i < matrix.Count; i++)
        {
            foreach (var j in matrix[i]) // i--j
            {
                if (j < i)
                {
                    continue;
                }
                foreach (var k in matrix[i]) // i--k
                {
                    if (k < j)
                    {
                        continue;
                    }
                    if (matrix[j].Contains(k))
                    {
                        triangles.Add(new Triangle(Vertexes[i], Vertexes[j], Vertexes[k]));
                    }
                }
            }
        }
        return triangles;
    }

    private static void Gradient(Point[] points, PathGradientBrush pgbrush, Triangle tre)
    {
        Color[] mySurroundColor = { tre.V1.CurrentColor, tre.V2.CurrentColor, tre.V3.CurrentColor };
        pgbrush.SurroundColors = mySurroundColor;
        var averageR = (tre.V1.CurrentColor.R + tre.V2.CurrentColor.R + tre.V3.CurrentColor.R) / 3;
        var averageG = (tre.V1.CurrentColor.G + tre.V2.CurrentColor.G + tre.V3.CurrentColor.G) / 3;
        var averageB = (tre.V1.CurrentColor.B + tre.V2.CurrentColor.B + tre.V3.CurrentColor.B) / 3;
        var centerCol = Color.FromArgb((byte)averageR, (byte)averageG, (byte)averageB);
        pgbrush.CenterColor = centerCol;
        pgbrush.SetBlendTriangularShape(0.7f, 0.7f);
        Graphics.FillPolygon(pgbrush, points);
        pgbrush.Dispose();
    }

    private void CheckVertexes()
    {

        for (var i = 0; i < Vertexes.Count; i++)
        {
            var p = new Point(Vertexes[i].X, Vertexes[i].Y);
            if (p.X > _lowerRightPointOfFragment.X || p.X < _upperLeftPointOfFragment.X || p.Y < _upperLeftPointOfFragment.Y || p.Y > _lowerRightPointOfFragment.Y)
            {
                Vertexes.RemoveAt(i);
            }
        }
        if (_upperLeftPointOfFragment != new Point(0, 0))
        {
            Vertexes.RemoveAll(x => x.X == 0 || x.Y == 0);
        }
        if (_lowerRightPointOfFragment != new Point(255, 255))
        {
            Vertexes.RemoveAll(x => x.X == 255 || x.Y == 255);
        }
    }

    private void Triangulation_Click(object sender, EventArgs e)
    {
        poligonalNet.Image = null;
        paintedPicture.Image = null;
        triangulatedPicture.Image = null;
        Vertexes.Clear();
        _triangles.Clear();
        GC.Collect();
        DivideImage.PerformClick();

        InitBitmap();

        if (Vertexes.Count == 0)
            MessageBox.Show(@"Триангуляция невозможна");
        else
        {
            Vertexes = Vertexes.Distinct().ToList();
            if (cutButton.Enabled == false)
            {
                CheckVertexes();
            }
            var matrix = new List<HashSet<int>>();
            for (var k = 0; k < Vertexes.Count; k++)
            {
                matrix.Add(new HashSet<int>());
            }

            Text = @"Запущен процесс триангуляции";

            int i = 0, j = 1;
            if (Vertexes[i].X < Vertexes[j].X)
            {
                var temp = i;
                i = j;
                j = temp;
            }
            for (var k = 2; k < Vertexes.Count; k++)
            {
                if (Vertexes[k].X > Vertexes[i].X)
                {
                    j = i;
                    i = k;
                }
                else
                {
                    if (Vertexes[k].X > Vertexes[j].X)
                    {
                        j = k;
                    }
                }
            }
            matrix[i].Add(j);
            matrix[j].Add(i);


            var qeVertexes1 = new Queue<int>();
            var qeVertexes2 = new Queue<int>();
            var qeDirection = new Queue<int>();
            qeVertexes1.Enqueue(i);
            qeVertexes2.Enqueue(j);
            qeDirection.Enqueue(0);
            while (qeVertexes1.Count != 0 && qeVertexes2.Count != 0)
            {
                Triangulate(qeVertexes1, qeVertexes2, qeDirection, matrix);
            }
            _triangles = GetTriangles(matrix);
            Text = @"Триангуляция Делоне";
            triangulatedPicture.Image = BmpLines;
            GC.Collect();

        }
    }

    private void Draw_Click(object sender, EventArgs e)
    {

        Text = @"Drawing in progress...";
        var points = new Point[3];
        var brushPath = new GraphicsPath();
        paintedPicture.Image = BmpLines;
        foreach (var t in _triangles)
        {
            points[0] = new Point(t.V1.X, t.V1.Y);
            points[1] = new Point(t.V2.X, t.V2.Y);
            points[2] = new Point(t.V3.X, t.V3.Y);
            brushPath.StartFigure();
            brushPath.AddLine(points[0], points[1]);
            brushPath.AddLine(points[0], points[2]);
            brushPath.AddLine(points[1], points[2]);
            brushPath.CloseFigure();
            try
            {
                using (var brush = new PathGradientBrush(brushPath))
                {
                    Gradient(points, brush, t);
                    brush.Dispose();
                    paintedPicture.Refresh();
                }
            }
            catch (OutOfMemoryException)
            {
                GC.Collect();
            }
        }
        Text = @"ImageChanger";
    }

    private void ExitButton_Click(object sender, EventArgs e)
    {
        var rsl = MessageBox.Show(@"Are you sure that you want to exit?", @"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (rsl == DialogResult.Yes)
        {
            Application.Exit();
        }
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        if (paintedPicture.Image != null)
        {
            var sv = new SaveFileDialog
            {
                Filter =
                    @"Image files (*.jpeg)| *.jpeg | Bitmap files (*.bmp)| *.bmp | Images (*.png)| *.png | Gif files (*.gif)| *.gif ",
                RestoreDirectory = true
            };
            if (sv.ShowDialog() != DialogResult.OK) return;
            var str = sv.OpenFile();
            try
            {
                switch (sv.FilterIndex)
                {
                    case 0: paintedPicture.Image.Save(str, ImageFormat.Jpeg); break;
                    case 1: paintedPicture.Image.Save(str, ImageFormat.Bmp); break;
                    case 2: paintedPicture.Image.Save(str, ImageFormat.Png); break;
                    case 3: paintedPicture.Image.Save(str, ImageFormat.Gif); break;
                    default: MessageBox.Show(@"Неправильный формат файла"); break;
                }
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }
            str.Close();
        }
        else MessageBox.Show(@"Нечего сохранять");
    }

    private void CleanButton_Click(object sender, EventArgs e)
    {
        Mem = originalPicture.Image;
        poligonalNet.Image = null;
        paintedPicture.Image = null;
        triangulatedPicture.Image = null;
        triangulationPoints.Image = null;
        fragmentPicture.Image = null;
        Vertexes.Clear();
        _triangles.Clear();
        _upperLeftPointOfFragment = new Point(0, 0);
        _lowerRightPointOfFragment = new Point(255, 255);
        GC.Collect();

    }

    private void CutButton_Click(object sender, EventArgs e)
    {
        try
        {
            _fragment = new Bitmap(256, 256);
            var original = new Bitmap(_currentImage);
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 256; j++)
                {
                    _fragment.SetPixel(i, j, Color.White);
                }
            }

            int x;
            for (x = _upperLeftPointOfFragment.X; x <= _lowerRightPointOfFragment.X; x++)
            {
                int y;
                for (y = _upperLeftPointOfFragment.Y; y <= _lowerRightPointOfFragment.Y; y++)
                {
                    _fragment.SetPixel(x, y, original.GetPixel(x, y));
                }
            }
            fragmentPicture.Image = _fragment;
            Mem = _fragment;
        }
        catch (NullReferenceException)
        {
            MessageBox.Show(@"Вы ничего не открыли");
        }
    }

    private void OriginalPicture_MouseDown(object sender, MouseEventArgs e)
    {
        _upperLeftPointOfFragment = e.Location;
    }

    private void OriginalPicture_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.X > _upperLeftPointOfFragment.X && e.Y > _upperLeftPointOfFragment.Y)
            _lowerRightPointOfFragment = e.Location;
        else
            _upperLeftPointOfFragment = new Point(0, 0);
    }
}
