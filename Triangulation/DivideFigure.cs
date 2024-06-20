namespace Triangulation;
internal abstract class DivideFigure
{
    protected int CurrHeight;          /// Store hight(Y) of current figure
    protected int CurrWidth;          /// Store wigth(X) of current figure
    protected int X;                  /// X coord for upper left point of current figure.
    protected int Y;                  /// Y coord for upper left point of current figure.
    protected Bitmap Bmp;

    /// Divide figure to smaller figures.
    protected void DrawLine(int x, int y, int currWidth, int currHight, bool vert)
    {
        Bmp = (Bitmap)AppForm.Mem;
        if (vert)
        {
            for (var i = y; i < currHight + Y; i++)
            {
                AppForm.BmpLines.SetPixel(currWidth + X, i, Color.Black);
                if (currHight != 1 && currHight != 2 || currWidth != 1 && currWidth != 2) continue;
                var curr = Bmp.GetPixel(currWidth + X, i);
                var m1 = new Vertex(currWidth + X, i, curr);
                AppForm.Vertexes.Add(m1);
            }
        }
        else
        {
            for (var i = x; i < currWidth + X; i++)
            {
                AppForm.BmpLines.SetPixel(i, currHight + Y, Color.Black);
                if ((currHight != 1 && currHight != 2) || (currWidth != 1 && currWidth != 2)) continue;
                var curr = Bmp.GetPixel(i, currHight + Y);
                var m1 = new Vertex(i, currHight + Y, curr);
                AppForm.Vertexes.Add(m1);
            }
        }
    }

    public bool CheckBrightness(int x, int y, int currWidth, int currHight, int bright)
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

    protected void CheckBrightness(int x, int y, int currWidth, int currHight)
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
        AppForm.Vertexes.Add(new Vertex(min.X, min.Y, Bmp.GetPixel(min.X, min.Y)));
        AppForm.Vertexes.Add(new Vertex(max.X, max.Y, Bmp.GetPixel(max.X, max.Y)));
    }
}
