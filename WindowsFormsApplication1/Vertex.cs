using System.Drawing;


// ReSharper disable once CheckNamespace
namespace Triangulation
{
    public class Vertex
    {
        public readonly int X;
        public readonly int Y;
        public Color CurrColor;

        public Vertex(int x, int y, Color curr)
        {
            X = x;
            Y = y;
            CurrColor = curr;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}
