using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;


// ReSharper disable once CheckNamespace
namespace Triangulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _upperLeftPointOfFragment = new Point(0, 0);
            _lowerRightPointOfFragment = new Point(255, 255);
        }

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

                if (rootRect.CheckBrightnes(0, 0, 256, 256, brightness))
                {
                    rootRect.DivideImage(brightness, rootRect);
                }

                Vertexes.Add(new Vertex(0, 0, ((Bitmap)Mem).GetPixel(0, 0)));
                Vertexes.Add(new Vertex(poligonalNet.Size.Width - 1, 0, ((Bitmap)Mem).GetPixel(poligonalNet.Size.Width - 1, 0)));
                Vertexes.Add(new Vertex(0, poligonalNet.Size.Height - 1, ((Bitmap)Mem).GetPixel(0, poligonalNet.Size.Height - 1)));
                Vertexes.Add(new Vertex(poligonalNet.Size.Width - 1, poligonalNet.Size.Height - 1, ((Bitmap)Mem).GetPixel(poligonalNet.Size.Width - 1, poligonalNet.Size.Height - 1)));
            
            foreach (var v in Vertexes)
            {
                BmpLines.SetPixel(v.X, v.Y, v.CurrColor);
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
                    temp.SetPixel(t.X, t.Y, t.CurrColor);
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
            Color[] mySurroundColor = { tre.V1.CurrColor, tre.V2.CurrColor, tre.V3.CurrColor };
            pgbrush.SurroundColors = mySurroundColor;
            var averageR = (tre.V1.CurrColor.R + tre.V2.CurrColor.R + tre.V3.CurrColor.R) / 3;
            var averageG = (tre.V1.CurrColor.G + tre.V2.CurrColor.G + tre.V3.CurrColor.G) / 3;
            var averageB = (tre.V1.CurrColor.B + tre.V2.CurrColor.B + tre.V3.CurrColor.B) / 3;
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

        private void Treangulation_Click(object sender, EventArgs e)
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

        private void saveButton_Click(object sender, EventArgs e)
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

        private void cleanButton_Click(object sender, EventArgs e)
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

        private void cutButton_Click(object sender, EventArgs e)
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

        private void originalPicture_MouseDown(object sender, MouseEventArgs e)
        {
            _upperLeftPointOfFragment = e.Location;
        }

        private void originalPicture_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X > _upperLeftPointOfFragment.X && e.Y > _upperLeftPointOfFragment.Y)
                _lowerRightPointOfFragment = e.Location;
            else
                _upperLeftPointOfFragment = new Point(0, 0);
        }


    }

    public class Vertex
    {
        public readonly int X;
        public readonly int Y;
        public Color CurrColor;
        private bool _free;

        public Vertex(int x, int y, Color curr)
        {
            X = x;
            Y = y;
            CurrColor = curr;
            _free = true;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }

    public class Triangle
    {
        public readonly Vertex V1;
        public readonly Vertex V2;
        public readonly Vertex V3;
        public Triangle(Vertex v1, Vertex v2, Vertex v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }
    }

    internal abstract class DivideFigure
    {
        protected int CurrHight;          /// Store hight(Y) of current figure
        protected int CurrWidth;          /// Store wigth(X) of current figure
        protected int X;                  /// X coord for upper left point of current figure.
        protected int Y;                  /// Y coord for upper left point of current figure.
        protected Bitmap Bmp;

        /// Divide figure to smaller figures.
        protected void DrawLine(int x, int y, int currWidth, int currHight, bool vert)
        {
            Bmp = (Bitmap)Form1.Mem;
            if (vert)
            {
                for (var i = y; i < currHight + Y; i++)
                {
                    Form1.BmpLines.SetPixel(currWidth + X, i, Color.Black);
                    if (currHight != 1 && currHight != 2 || currWidth != 1 && currWidth != 2) continue;
                    var curr = Bmp.GetPixel(currWidth + X, i);
                    var m1 = new Vertex(currWidth + X, i, curr);
                    Form1.Vertexes.Add(m1);
                }
            }
            else
            {
                for (var i = x; i < currWidth + X; i++)
                {
                    Form1.BmpLines.SetPixel(i, currHight + Y, Color.Black);
                    if ((currHight != 1 && currHight != 2) || (currWidth != 1 && currWidth != 2)) continue;
                    var curr = Bmp.GetPixel(i, currHight + Y);
                    var m1 = new Vertex(i, currHight + Y, curr);
                    Form1.Vertexes.Add(m1);
                }
            }
        }

        public bool CheckBrightnes(int x, int y, int currWidth, int currHight, int bright)
        {
            float r = (11 * Bmp.GetPixel(x, y).B + 30 * Bmp.GetPixel(x, y).R + 59 * Bmp.GetPixel(x, y).G) / 100;
            for (var i = x; i < (currWidth + x); i++)
            {
                for (var j = y; j < (currHight + y); j++)
                {
                    var curr = Bmp.GetPixel(i, j);
                    float r1 = (11 * curr.B + 30 * curr.R + 59 * curr.G) / 100;
                    if (r - r1 >= bright)
                    {
                        return true;
                    }
                    if (r1 > r)
                    {
                        r = r1;
                    }
                }
            }
            return false;
        }

        protected void CheckBrightnes(int x, int y, int currWidth, int currHight)
        {
            float minBright = (11 * Bmp.GetPixel(x, y).B + 30 * Bmp.GetPixel(x, y).R + 59 * Bmp.GetPixel(x, y).G) / 100;
            var maxBright = minBright;
            var min = new Point();
            var max = new Point();
            for (var i = x; i < (currWidth + x); i++)
            {
                for (var j = y; j < (currHight + y); j++)
                {
                    var curr = Bmp.GetPixel(i, j);
                    float currBright = (11 * curr.B + 30 * curr.R + 59 * curr.G) / 100;
                    if (currBright < minBright)
                    {
                        minBright = currBright;
                        min.X = i;
                        min.Y = j;
                    }

                    if (!(currBright > maxBright)) continue;
                    maxBright = currBright;
                    max.X = i;
                    max.Y = j;
                }
            }
            Form1.Vertexes.Add(new Vertex(min.X, min.Y, Bmp.GetPixel(min.X, min.Y)));
            Form1.Vertexes.Add(new Vertex(max.X, max.Y, Bmp.GetPixel(max.X, max.Y)));
        }
    }

    internal sealed class Rectangle : DivideFigure
    {
        private readonly Form1 _form;
        private readonly bool _lineVert;                     /// Store if this figure need to divide horisontal or vertical line
        private readonly Rectangle _parent;

        public Rectangle(Form1 form, int x, int y, int currWidth, int currHight, bool lineVert, Rectangle parent)
        {
            _form = form;
            X = x;
            Y = y;
            CurrHight = currHight;
            CurrWidth = currWidth;
            _lineVert = lineVert;
            Bmp = (Bitmap)Form1.Mem;
            _parent = parent;
        }

        public void DivideImage(int bright, Rectangle parent)
        {
            if (_lineVert)
            {
                CurrWidth = CurrWidth / 2;
                var r = new Rectangle(_form, X, Y, CurrWidth, CurrHight, false, parent);
                
                if (r.CheckBrightnes(X, Y, CurrWidth, CurrHight, bright))
                {
                    DrawLine(CurrWidth, Y, CurrWidth, CurrHight, true);
                    _form.GetSecondPb().Image = Form1.BmpLines;
                    r.DivideImage(bright, r);
                }
                
                var r1 = new Rectangle(_form, X + CurrWidth, Y, CurrWidth, CurrHight, false, parent);
                if (!r1.CheckBrightnes(X + CurrWidth, Y, CurrWidth, CurrHight, bright)) return;
                DrawLine(CurrWidth, Y, CurrWidth, CurrHight, true);
                r1.DivideImage(bright, r1);
            }
            else
            {
                CurrHight = CurrHight / 2;
                var r = new Rectangle(_form, X, Y, CurrWidth, CurrHight, true, parent);
                if (r.CheckBrightnes(X, Y, CurrWidth, CurrHight, bright))
                {
                    DrawLine(X, CurrHight, CurrWidth, CurrHight, false);
                    _form.GetSecondPb().Image = Form1.BmpLines;
                    r.DivideImage(bright, parent);
                }
                else
                {
                    r.CheckBrightnes(X, Y, CurrWidth, CurrHight);
                }

                var r1 = new Rectangle(_form, X, Y + CurrHight, CurrWidth, CurrHight, true, _parent);
                if (r1.CheckBrightnes(X, (Y + CurrHight), CurrWidth, CurrHight, bright))
                {
                    DrawLine(X, CurrHight + Y, CurrWidth, CurrHight, false);
                    Form1.MarkRed(X, Y + CurrHight);
                    r1.DivideImage(bright, parent);
                }
                else
                {
                    r1.CheckBrightnes(X, (Y + CurrHight), CurrWidth, CurrHight);
                }

            }
        }
    }
    }
