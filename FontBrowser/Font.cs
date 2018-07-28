using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace FontBrowser
{
    public class Font
    {
        private Lazy<List<Glyph>> _glyphs;
        private Lazy<List<FontProperty>> _properties;

        public Font(Typeface face, string name, GlyphTypeface gface)
        {
            Face = face;
            FaceName = name;
            GlyphTypeface = gface;
            _glyphs = new Lazy<List<Glyph>>(LoadGlyphs);
            _properties = new Lazy<List<FontProperty>>(LoadProperties);
        }

        public string FaceName { get; }
        public Typeface Face { get; }
        public string Name => string.Join(", ", Face.FontFamily.FamilyNames.Select(n => n.Value)) + " " + FaceName;
        public GlyphTypeface GlyphTypeface { get; }
        public IReadOnlyList<Glyph> Glyphs => _glyphs.Value;
        public IReadOnlyList<FontProperty> Properties => _properties.Value;

        public override string ToString() => Name;

        private List<Glyph> LoadGlyphs()
        {
            var dic = new Dictionary<ushort, int>();
            foreach (var kv in GlyphTypeface.CharacterToGlyphMap)
            {
                dic[kv.Value] = kv.Key;
            }

            var list = new List<Glyph>();
            for (int i = 0; i < GlyphTypeface.GlyphCount; i++)
            {
                if (dic.TryGetValue((ushort)i, out int cp))
                {
                    var geo = GlyphTypeface.GetGlyphOutline((ushort)i, 10, 10);
                    var g = new Glyph(geo, cp);
                    list.Add(g);
                }
            }
            return list;
        }

        private List<FontProperty> LoadProperties()
        {
            var list = new List<FontProperty>();
            list.Add(new FontProperty("Name", Name));
            list.Add(new FontProperty(nameof(GlyphTypeface.Version), GlyphTypeface.Version));
            list.Add(new FontProperty("Baseline", GlyphTypeface.Baseline));
            list.Add(new FontProperty("Glyphs Count", GlyphTypeface.GlyphCount));
            list.Add(new FontProperty(nameof(GlyphTypeface.Height), GlyphTypeface.Height));
            list.Add(new FontProperty("Caps Height", GlyphTypeface.CapsHeight));
            list.Add(new FontProperty(nameof(GlyphTypeface.Stretch), GlyphTypeface.Stretch));
            list.Add(new FontProperty(nameof(GlyphTypeface.XHeight), GlyphTypeface.XHeight));
            list.Add(new FontProperty(nameof(GlyphTypeface.Style), GlyphTypeface.Style));
            list.Add(new FontProperty("Style Simulations", GlyphTypeface.StyleSimulations));

            if (GlyphTypeface.Copyrights.Count > 0)
            {
                list.Add(new FontProperty("Copyright", string.Join(", ", GlyphTypeface.Copyrights.Select(k => k.Value))));
            }

            if (GlyphTypeface.Descriptions.Count > 0)
            {
                list.Add(new FontProperty("Description", string.Join(", ", GlyphTypeface.Descriptions.Select(k => k.Value))));
            }

            if (GlyphTypeface.DesignerNames.Count > 0)
            {
                list.Add(new FontProperty("Designers", string.Join(", ", GlyphTypeface.DesignerNames.Select(k => k.Value))));
            }

            if (GlyphTypeface.ManufacturerNames.Count > 0)
            {
                list.Add(new FontProperty("Manufacturer", string.Join(", ", GlyphTypeface.ManufacturerNames.Select(k => k.Value))));
            }

            if (GlyphTypeface.Trademarks.Count > 0)
            {
                list.Add(new FontProperty("Trademark", string.Join(", ", GlyphTypeface.Trademarks.Select(k => k.Value))));
            }

            if (GlyphTypeface.LicenseDescriptions.Count > 0)
            {
                list.Add(new FontProperty("License", string.Join(", ", GlyphTypeface.LicenseDescriptions.Select(k => k.Value))));
            }

            list.Add(new FontProperty("Strikethrough Position", GlyphTypeface.StrikethroughPosition));
            list.Add(new FontProperty("Strikethrough Thickness", GlyphTypeface.StrikethroughThickness));
            list.Add(new FontProperty("Underline Position", GlyphTypeface.UnderlinePosition));
            list.Add(new FontProperty("Underline Thickness", GlyphTypeface.UnderlineThickness));
            list.Add(new FontProperty("Embedding Rights", GlyphTypeface.EmbeddingRights));
            list.Add(new FontProperty(nameof(GlyphTypeface.Symbol), GlyphTypeface.Symbol));
            list.Add(new FontProperty(nameof(GlyphTypeface.Weight), GlyphTypeface.Weight));
            return list;
        }
    }
}
