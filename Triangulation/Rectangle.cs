using System.Drawing;

namespace Triangulation;
internal sealed class Rectangle : DivideFigure
{
    private readonly AppForm _form;
    private readonly bool _lineVert;                     /// Store if this figure need to divide horisontal or vertical line
    private readonly Rectangle _parent;

    public Rectangle(AppForm form, int x, int y, int currWidth, int currHeight, bool lineVert, Rectangle parent)
    {
        _form = form;
        X = x;
        Y = y;
        CurrHeight = currHeight;
        CurrWidth = currWidth;
        _lineVert = lineVert;
        Bmp = (Bitmap)AppForm.Mem;
        _parent = parent;
    }

    public void DivideImage(int bright, Rectangle parent)
    {
        if (_lineVert)
        {
            CurrWidth /= 2;
            var r = new Rectangle(_form, X, Y, CurrWidth, CurrHeight, false, parent);

            if (r.CheckBrightness(X, Y, CurrWidth, CurrHeight, bright))
            {
                DrawLine(CurrWidth, Y, CurrWidth, CurrHeight, true);
                _form.GetSecondPb().Image = AppForm.BmpLines;
                r.DivideImage(bright, r);
            }

            var r1 = new Rectangle(_form, X + CurrWidth, Y, CurrWidth, CurrHeight, false, parent);
            if (!r1.CheckBrightness(X + CurrWidth, Y, CurrWidth, CurrHeight, bright)) return;
            DrawLine(CurrWidth, Y, CurrWidth, CurrHeight, true);
            r1.DivideImage(bright, r1);
        }
        else
        {
            CurrHeight /= 2;
            var r = new Rectangle(_form, X, Y, CurrWidth, CurrHeight, true, parent);
            if (r.CheckBrightness(X, Y, CurrWidth, CurrHeight, bright))
            {
                DrawLine(X, CurrHeight, CurrWidth, CurrHeight, false);
                _form.GetSecondPb().Image = AppForm.BmpLines;
                r.DivideImage(bright, parent);
            }
            else
            {
                r.CheckBrightness(X, Y, CurrWidth, CurrHeight);
            }

            var r1 = new Rectangle(_form, X, Y + CurrHeight, CurrWidth, CurrHeight, true, _parent);
            if (r1.CheckBrightness(X, (Y + CurrHeight), CurrWidth, CurrHeight, bright))
            {
                DrawLine(X, CurrHeight + Y, CurrWidth, CurrHeight, false);
                AppForm.MarkRed(X, Y + CurrHeight);
                r1.DivideImage(bright, parent);
            }
            else
            {
                r1.CheckBrightness(X, (Y + CurrHeight), CurrWidth, CurrHeight);
            }

        }
    }
}
