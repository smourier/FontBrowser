using System.Linq;
using System.Windows.Media;

namespace FontBrowser
{
    public class Font
    {
        public Font(Typeface face, string name)
        {
            Face = face;
            FaceName = name;
        }

        public string FaceName { get; }
        public Typeface Face { get; }
        public string Name => string.Join(", ", Face.FontFamily.FamilyNames.Select(n => n.Value)) + " " + FaceName;

        public override string ToString() => Name;
    }
}
