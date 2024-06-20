using System.Drawing;

namespace Triangulation;

public record Vertex(int X, int Y, Color CurrentColor) {
    public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
}
