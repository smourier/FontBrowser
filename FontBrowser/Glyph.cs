using System.Windows.Media;

namespace FontBrowser
{
    public class Glyph
    {
        public Glyph(Geometry geometry, int codePoint)
        {
            Geometry = geometry;
            CodePoint = codePoint;
        }

        public Geometry Geometry { get; }
        public int CodePoint { get; }
        public string CodePointText => CodePoint + ", 0x" + CodePoint.ToString("X4");
    }
}
