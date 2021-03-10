// ReSharper disable once CheckNamespace
namespace Triangulation
{
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
}
