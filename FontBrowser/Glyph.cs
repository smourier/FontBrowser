using System;
using System.Windows.Media;

namespace FontBrowser
{
    public class Glyph : IComparable, IComparable<Glyph>
    {
        public Glyph(Geometry geometry, int codePoint)
        {
            Geometry = geometry;
            CodePoint = codePoint;
        }

        public Geometry Geometry { get; }
        public int CodePoint { get; }
        public string CodePointText => CodePoint + ", 0x" + CodePoint.ToString("X4");

        public override string ToString() => CodePointText;

        int IComparable.CompareTo(object obj) => CompareTo(obj as Glyph);
        public int CompareTo(Glyph other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return CodePoint.CompareTo(other.CodePoint);
        }
    }
}
