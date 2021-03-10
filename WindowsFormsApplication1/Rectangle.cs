using System.Drawing;

namespace Triangulation
{
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
                CurrWidth /= 2;
                var r = new Rectangle(_form, X, Y, CurrWidth, CurrHight, false, parent);

                if (r.CheckBrightness(X, Y, CurrWidth, CurrHight, bright))
                {
                    DrawLine(CurrWidth, Y, CurrWidth, CurrHight, true);
                    _form.GetSecondPb().Image = Form1.BmpLines;
                    r.DivideImage(bright, r);
                }

                var r1 = new Rectangle(_form, X + CurrWidth, Y, CurrWidth, CurrHight, false, parent);
                if (!r1.CheckBrightness(X + CurrWidth, Y, CurrWidth, CurrHight, bright)) return;
                DrawLine(CurrWidth, Y, CurrWidth, CurrHight, true);
                r1.DivideImage(bright, r1);
            }
            else
            {
                CurrHight /= 2;
                var r = new Rectangle(_form, X, Y, CurrWidth, CurrHight, true, parent);
                if (r.CheckBrightness(X, Y, CurrWidth, CurrHight, bright))
                {
                    DrawLine(X, CurrHight, CurrWidth, CurrHight, false);
                    _form.GetSecondPb().Image = Form1.BmpLines;
                    r.DivideImage(bright, parent);
                }
                else
                {
                    r.CheckBrightness(X, Y, CurrWidth, CurrHight);
                }

                var r1 = new Rectangle(_form, X, Y + CurrHight, CurrWidth, CurrHight, true, _parent);
                if (r1.CheckBrightness(X, (Y + CurrHight), CurrWidth, CurrHight, bright))
                {
                    DrawLine(X, CurrHight + Y, CurrWidth, CurrHight, false);
                    Form1.MarkRed(X, Y + CurrHight);
                    r1.DivideImage(bright, parent);
                }
                else
                {
                    r1.CheckBrightness(X, (Y + CurrHight), CurrWidth, CurrHight);
                }

            }
        }
    }
}
