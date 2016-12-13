using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Permissions;
using System.Drawing.Imaging;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            startFragment = new Point(0, 0);
            finishFragment = new Point(255, 255);
        }

        public Image currImage;
        static public Image Mem;
        Point startFragment;
        Point finishFragment;
        Bitmap fragment;

        private void Open_Click(object sender, EventArgs e)
        {
            cleanButton.PerformClick();
            Stream str = null;
            OpenFileDialog op = new OpenFileDialog();
            Bitmap bm;
            op.Filter = "Image files|*.jpeg; *.jpg; *.bmp; *.png; *.gif";
            op.RestoreDirectory = true;
            if (op.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((str = op.OpenFile()) != null)
                    {
                        using (str)
                        {
                            
                            Size sz = new Size(256, 256);
                            bm = new Bitmap(str);
                            Bitmap tm = new Bitmap(bm, sz);
                            pictureBox1.Image = tm;
                            currImage = tm;
                            Mem = tm;
                            
                        }
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        public PictureBox getSecondPB()
        {
            return pictureBox2;
        }

        public PictureBox getFirstPB()
        {
            return pictureBox1;
        }

        public static List<vertex> vertexes = new List<vertex>();
        public List<Triangle> triangles = new List<Triangle>();
        public static Bitmap bmpLines = new Bitmap(256, 256);
        public static Graphics graphics = Graphics.FromImage(bmpLines);

        public static void initBitmap()
        {
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 256; j++)
                {
                    bmpLines.SetPixel(i, j, Color.White);
                }
            }
        }

        public static void markRed(int x, int y)
        {
            Bitmap bmp = (Bitmap)Form1.Mem;
            bmpLines.SetPixel(x, y, Color.Red);
            Color curr = bmp.GetPixel(x, y);
            Form1.vertexes.Add(new vertex(x, y, curr));
        }

        public List<vertex> deleteVertexes(List<vertex> vertexes)
        {
            for (int i = 3; (i < vertexes.Count - 3); i++)
            {
                for (int j = 0; (j < vertexes.Count); j++)
                {
                    if ((Math.Abs(vertexes[i].x - vertexes[j].x) < 2) && (Math.Abs(vertexes[i].y - vertexes[j].y) < 2))
                    {
                        vertexes.RemoveAt(j);
                    }
                }
            }
            return vertexes;
        }

        private void DivImage_Click(object sender, EventArgs e)
        {
            try
            {
                Rectangle rootRect = new Rectangle(this, 0, 0, 256, 256, true, null);
                initBitmap();
                int brightness = (int)numericUpDown1.Value;

                if (rootRect.checkBrightnes(0, 0, 256, 256, brightness))
                {
                    rootRect.divideImage(brightness, rootRect);
                }

                vertexes.Add(new vertex(0, 0, ((Bitmap)Mem).GetPixel(0, 0)));
                vertexes.Add(new vertex(pictureBox2.Size.Width - 1, 0, ((Bitmap)Mem).GetPixel(pictureBox2.Size.Width - 1, 0)));
                vertexes.Add(new vertex(0, pictureBox2.Size.Height - 1, ((Bitmap)Mem).GetPixel(0, pictureBox2.Size.Height - 1)));
                vertexes.Add(new vertex(pictureBox2.Size.Width - 1, pictureBox2.Size.Height - 1, ((Bitmap)Mem).GetPixel(pictureBox2.Size.Width - 1, pictureBox2.Size.Height - 1)));
            
            foreach (vertex v in vertexes)
            {
                bmpLines.SetPixel(v.x, v.y, v.currColor);
            }

            pictureBox2.Image = bmpLines;
            pictureBox2.Refresh();
            vertexes = deleteVertexes(vertexes);
            GC.Collect();
            showTops();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Ошибка: файл не открыт");
            }
        }
        public void showTops()
        {
            Bitmap temp = new Bitmap(256, 256);
            try
            {
                for (var i = 0; i < 256; i++)
                {
                    for (var j = 0; j < 256; j++)
                    {
                        temp.SetPixel(i, j, Color.White);
                    }
                }
                
                for (int i = 0; i < vertexes.Count; i++)
                {
                    temp.SetPixel(vertexes[i].x, vertexes[i].y, vertexes[i].currColor);
                }
                this.triangulationPoints.Image = temp;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Что-то тут явно не так");
            }
        }
        public void drawSide(vertex begin, vertex end)
        {
            graphics.DrawLine(new Pen(Color.Black), new Point(begin.x, begin.y), new Point(end.x, end.y));
        }

        private int getVertexDir(vertex first, vertex second, vertex third)
        {
            int turn = (third.x - first.x) * (second.y - first.y) - (third.y - first.y) * (second.x - first.x);
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

        public void triangulate(Queue<int> qeVertexes1, Queue<int> qeVertexes2, Queue<int> qeDirection, List<HashSet<int>> matrix)
        {
            int f = qeVertexes1.Dequeue();
            int s = qeVertexes2.Dequeue();
            vertex first = vertexes[f];
            vertex second = vertexes[s];
            int dir = qeDirection.Dequeue();
            drawSide(first, second);
            bool checkVert = false;
            double min = 2;
            int n = 0;
            for (int m = 0; m < vertexes.Count; m++)
            {
                if ((dir != 0 && getVertexDir(first, second, vertexes[m]) == dir))
                {
                    continue;
                }

                if (matrix[f].Contains(m) && matrix[s].Contains(m))
                {
                    return;
                }

                double fs = Math.Pow(first.x - second.x, 2) + Math.Pow(first.y - second.y, 2);
                double fm = Math.Pow(first.x - vertexes[m].x, 2) + Math.Pow(first.y - vertexes[m].y, 2);
                double ms = Math.Pow(vertexes[m].x - second.x, 2) + Math.Pow(vertexes[m].y - second.y, 2);
                double curr = (-fs + fm + ms) / (2 * Math.Sqrt(fm * ms));
                if (curr < min)
                {
                    min = curr;
                    checkVert = true;
                    n = m;
                }
            }

            if (checkVert)
            {
                matrix[f].Add(n);
                matrix[s].Add(n);
                matrix[n].Add(f);
                matrix[n].Add(s);
                drawSide(first, vertexes[n]);
                drawSide(second, vertexes[n]);
                qeVertexes1.Enqueue(f);
                qeVertexes2.Enqueue(n);
                qeVertexes1.Enqueue(n);
                qeVertexes2.Enqueue(s);
                qeDirection.Enqueue(-getVertexDir(first, second, vertexes[n]));
                qeDirection.Enqueue(-getVertexDir(first, second, vertexes[n]));
            }
        }

        List<Triangle> getTriangles(List<HashSet<int>> matrix)
        {
            
            List<Triangle> triangles = new List<Triangle>();
            
            for (int i = 0; i < matrix.Count; i++)
            {
                foreach (int j in matrix[i]) // i--j
                {
                    if (j < i)
                    {
                        continue;
                    }
                    foreach (int k in matrix[i]) // i--k
                    {
                        if (k < j)
                        {
                            continue;
                        }
                        if (matrix[j].Contains(k))
                        {
                            triangles.Add(new Triangle(vertexes[i], vertexes[j], vertexes[k]));
                        }
                    }
                }
            }
            return triangles;
        }

        public void gradient(Point[] points, PathGradientBrush pgbrush, Triangle tre, GraphicsPath brushPath)
        {
            Color[] mySurroundColor = { tre.v1.currColor, tre.v2.currColor, tre.v3.currColor };
            pgbrush.SurroundColors = mySurroundColor;
            int averageR = (tre.v1.currColor.R + tre.v2.currColor.R + tre.v3.currColor.R) / 3;
            int averageG = (tre.v1.currColor.G + tre.v2.currColor.G + tre.v3.currColor.G) / 3;
            int averageB = (tre.v1.currColor.B + tre.v2.currColor.B + tre.v3.currColor.B) / 3;
            Color centerCol = Color.FromArgb((byte)averageR, (byte)averageG, (byte)averageB);
            pgbrush.CenterColor = centerCol;
            pgbrush.SetBlendTriangularShape(0.7f, 0.7f);
            graphics.FillPolygon(pgbrush, points);
            pgbrush.Dispose();
        }

        public void checkVertexes()
        {
            
            for (int i = 0; i < vertexes.Count; i++)
            {
                Point p = new Point(vertexes[i].x, vertexes[i].y);
                if (p.X > finishFragment.X || p.X < startFragment.X || p.Y < startFragment.Y || p.Y > finishFragment.Y)
                {
                    vertexes.RemoveAt(i);
                }
            }
            if (startFragment != new Point(0, 0))
            {
                vertexes.RemoveAll(x => x.x == 0 || x.y == 0);
            }
            if (finishFragment != new Point(255, 255))
            {
                vertexes.RemoveAll(x => x.x == 255 || x.y == 255);
            }
        }

        private void Treangulation_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox5.Image = null;
            vertexes.Clear();
            triangles.Clear();
            GC.Collect();
            DivImage.PerformClick();
            
            Form1.initBitmap();
            Bitmap bmpTre = bmpLines;

            if (vertexes.Count == 0)
                MessageBox.Show("Триангуляция невозможна");
            else
            {
                vertexes = vertexes.Distinct().ToList();
                if (cutButton.Enabled == false)
                {
                    checkVertexes();
                }
                List<HashSet<int>> matrix = new List<HashSet<int>>();
                for (int k = 0; k < vertexes.Count; k++)
                {
                    matrix.Add(new HashSet<int>());
                }

                this.Text = "Triangulation in progress...";

                int i = 0, j = 1;
                if (vertexes[i].x < vertexes[j].x)
                {
                    int temp = i;
                    i = j;
                    j = temp;
                }
                for (int k = 2; k < vertexes.Count; k++)
                {
                    if (vertexes[k].x > vertexes[i].x)
                    {
                        j = i;
                        i = k;
                    }
                    else
                    {
                        if (vertexes[k].x > vertexes[j].x)
                        {
                            j = k;
                        }
                    }
                }
                matrix[i].Add(j);
                matrix[j].Add(i);


                Queue<int> qeVertexes1 = new Queue<int>();
                Queue<int> qeVertexes2 = new Queue<int>();
                Queue<int> qeDirection = new Queue<int>();
                qeVertexes1.Enqueue(i);
                qeVertexes2.Enqueue(j);
                qeDirection.Enqueue(0);
                while (qeVertexes1.Count() != 0 && qeVertexes2.Count() != 0)
                {
                    triangulate(qeVertexes1, qeVertexes2, qeDirection, matrix);
                }
                triangles = getTriangles(matrix);
                this.Text = "ImageChanger";
                pictureBox5.Image = bmpLines;
                GC.Collect();
                
            }
        }

        private void Draw_Click(object sender, EventArgs e)
        {
            
            this.Text = "Drawing in progress...";
            Point[] points = new Point[3];
            GraphicsPath brushPath = new GraphicsPath();
            pictureBox3.Image = bmpLines;
            for (int i = 0; i < triangles.Count; i++)
            {
                points[0] = new Point(triangles[i].v1.x, triangles[i].v1.y);
                points[1] = new Point(triangles[i].v2.x, triangles[i].v2.y);
                points[2] = new Point(triangles[i].v3.x, triangles[i].v3.y);
                brushPath.StartFigure();
                brushPath.AddLine(points[0], points[1]);
                brushPath.AddLine(points[0], points[2]);
                brushPath.AddLine(points[1], points[2]);
                brushPath.CloseFigure();
                try
                {
                    using (var brush = new PathGradientBrush(brushPath))
                    {
                        gradient(points, brush, triangles[i], brushPath);
                        brush.Dispose();
                        pictureBox3.Refresh();
                    }
                }
                catch (OutOfMemoryException)
                {
                    GC.Collect();
                }
            }
            this.Text = "ImageChanger";
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult rsl = MessageBox.Show("Are you sure that you want to exit?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rsl == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image != null)
            {
                SaveFileDialog sv = new SaveFileDialog();
                Stream str;
                sv.Filter = "Image files (*.jpeg)| *.jpeg | Bitmap files (*.bmp)| *.bmp | Images (*.png)| *.png | Gif files (*.gif)| *.gif ";
                sv.RestoreDirectory = true;
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    if ((str = sv.OpenFile()) != null)
                    {
                        try
                        {
                            switch (sv.FilterIndex)
                            {
                                case 0: pictureBox3.Image.Save(str, ImageFormat.Jpeg); break;
                                case 1: pictureBox3.Image.Save(str, ImageFormat.Bmp); break;
                                case 2: pictureBox3.Image.Save(str, ImageFormat.Png); break;
                                case 3: pictureBox3.Image.Save(str, ImageFormat.Gif); break;
                                default: MessageBox.Show("Неправильный формат файла"); break;
                            }
                        }
                        catch (Exception s)
                        {
                            MessageBox.Show(s.Message);
                        }
                        str.Close();
                    }
                }
            }
            else MessageBox.Show("Нечего сохранять");
        }

        private void cleanButton_Click(object sender, EventArgs e)
        {
            Mem = pictureBox1.Image;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox5.Image = null;
            triangulationPoints.Image = null;
            fragmentPic.Image = null;
            vertexes.Clear();
            triangles.Clear();
            startFragment = new Point(0, 0);
            finishFragment = new Point(255, 255);
            GC.Collect();
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            startFragment = e.Location;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X > startFragment.X && e.Y > startFragment.Y)
                finishFragment = e.Location;
            else
                startFragment = new Point(0, 0);
            
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            try
            {
                fragment = new Bitmap(256, 256);
                Bitmap original = new Bitmap(currImage);
                for (var i = 0; i < 256; i++)
                {
                    for (var j = 0; j < 256; j++)
                    {
                        fragment.SetPixel(i, j, Color.White);
                    }
                }
                int x, y;
                for (x = startFragment.X; x <= finishFragment.X; x++)
                {
                    for (y = startFragment.Y; y <= finishFragment.Y; y++)
                    {
                        fragment.SetPixel(x, y, original.GetPixel(x, y));
                    }
                }
                fragmentPic.Image = fragment;
                Mem = fragment;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Вы ничего не открыли");
            }
        }

    }

    public class vertex
    {
        public int x;
        public int y;
        public Color currColor;
        public bool free;

        public vertex(int _x, int _y, Color _curr)
        {
            x = _x;
            y = _y;
            currColor = _curr;
            free = true;
        }

        public bool Equals(vertex other)
        {
            return ((this.x == other.x) && (this.y == other.y));
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
    };

    public class Triangle
    {
        public vertex v1;
        public vertex v2;
        public vertex v3;
        public Triangle(vertex _v1, vertex _v2, vertex _v3)
        {
            v1 = _v1;
            v2 = _v2;
            v3 = _v3;
        }
    };

    abstract class DivideFigure
    {
        public static int fullSize = 256;
        protected int currHight;          /// Store hight(Y) of current figure
        protected int currWidth;          /// Store wigth(X) of current figure
        protected int x;                  /// X coord for upper left point of current figure.
        protected int y;                  /// Y coord for upper left point of current figure.
        public Bitmap bmp;

        abstract public void divideImage(int bright, Rectangle _parent);        /// Divide figure to smaller figures.

        public void drawLine(int _x, int _y, int _currWidth, int _currHight, bool vert)                     ///
        {
            bmp = (Bitmap)Form1.Mem;
            if (vert)
            {
                for (int i = _y; i < _currHight + y; i++)
                {
                    Form1.bmpLines.SetPixel((_currWidth + x), i, Color.Black);
                    if ((_currHight == 1 || _currHight == 2) && (_currWidth == 1 || _currWidth == 2))
                    {
                        Color curr = bmp.GetPixel((_currWidth + x), i);
                        vertex M1 = new vertex((_currWidth + x), i, curr);
                        Form1.vertexes.Add(M1);
                    }
                }
            }
            else
            {
                for (int i = _x; i < _currWidth + x; i++)
                {
                    Form1.bmpLines.SetPixel(i, _currHight + y, Color.Black);
                    if ((_currHight == 1 || _currHight == 2) && (_currWidth == 1 || _currWidth == 2))
                    {
                        Color curr = bmp.GetPixel(i, _currHight + y);
                        vertex M1 = new vertex(i, _currHight + y, curr);
                        Form1.vertexes.Add(M1);
                      
                    }
                }
            }
        }

        public bool checkBrightnes(int x, int y, int currWidth, int currHight, int bright)    /// Method that finds two points in figure, that have difference in brightness more than needed.
        {
            float r = (11 * bmp.GetPixel(x, y).B + 30 * bmp.GetPixel(x, y).R + 59 * bmp.GetPixel(x, y).G) / 100;
            for (var i = x; i < (currWidth + x); i++)
            {
                for (var j = y; j < (currHight + y); j++)
                {
                    Color curr = bmp.GetPixel(i, j);
                    float r1 = (11 * curr.B + 30 * curr.R + 59 * curr.G) / 100;
                    if ((r - r1) >= bright)
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
        public void checkBrightnes(int x, int y, int currWidth, int currHight)
        {
            float minBright = (11 * bmp.GetPixel(x, y).B + 30 * bmp.GetPixel(x, y).R + 59 * bmp.GetPixel(x, y).G) / 100;
            float maxBright = minBright;
            Point min = new Point();
            Point max = new Point();
            for (var i = x; i < (currWidth + x); i++)
            {
                for (var j = y; j < (currHight + y); j++)
                {
                    Color curr = bmp.GetPixel(i, j);
                    float currBright = (11 * curr.B + 30 * curr.R + 59 * curr.G) / 100;
                    if (currBright < minBright)
                    {
                        minBright = currBright;
                        min.X = i;
                        min.Y = j;
                    }
                    if (currBright > maxBright)
                    {
                        maxBright = currBright;
                        max.X = i;
                        max.Y = j;
                    }
                }
            }
            Form1.vertexes.Add(new vertex(min.X, min.Y, bmp.GetPixel(min.X, min.Y)));
            Form1.vertexes.Add(new vertex(max.X, max.Y, bmp.GetPixel(max.X, max.Y)));
        }
    };

    class Rectangle : DivideFigure
    {
        private Form1 form;
        private bool lineVert;                     /// Store if this figure need to divide horisontal or vertical line
        private Rectangle parent;

        public Rectangle(Form1 _form, int _x, int _y, int _currWidth, int _currHight, bool _lineVert, Rectangle _parent)
        {
            this.form = _form;
            this.x = _x;
            this.y = _y;
            this.currHight = _currHight;
            this.currWidth = _currWidth;
            this.lineVert = _lineVert;
            this.bmp = (Bitmap)Form1.Mem;
            this.parent = _parent;
        }

        public override void divideImage(int bright, Rectangle _parent)
        {
            if (lineVert)
            {
                currWidth = currWidth / 2;
                var R = new Rectangle(form, x, y, currWidth, currHight, false, _parent);
                
                if (R.checkBrightnes(x, y, currWidth, currHight, bright))
                {
                    drawLine(currWidth, y, currWidth, currHight, true);
                    form.getSecondPB().Image = Form1.bmpLines;
                    R.divideImage(bright, R);
                }
                
                var R1 = new Rectangle(form, x + currWidth, y, currWidth, currHight, false, _parent);
                if (R1.checkBrightnes((x + currWidth), y, currWidth, currHight, bright))
                {
                    drawLine(currWidth, y, currWidth, currHight, true);
                    R1.divideImage(bright, R1);
                }
            }
            else
            {
                currHight = currHight / 2;
                var R = new Rectangle(form, x, y, currWidth, currHight, true, _parent);
                if (R.checkBrightnes(x, y, currWidth, currHight, bright))
                {
                    drawLine(x, currHight, currWidth, currHight, false);
                    form.getSecondPB().Image = Form1.bmpLines;
                    R.divideImage(bright, _parent);
                }
                else
                {
                    R.checkBrightnes(x, y, currWidth, currHight);
                }

                var R1 = new Rectangle(form, x, y + currHight, currWidth, currHight, true, parent);
                if (R1.checkBrightnes(x, (y + currHight), currWidth, currHight, bright))
                {
                    drawLine(x, currHight + y, currWidth, currHight, false);
                    Form1.markRed(x, y + currHight);
                    R1.divideImage(bright, _parent);
                }
                else
                {
                    R1.checkBrightnes(x, (y + currHight), currWidth, currHight);
                }

            }
        }
    };
    }
