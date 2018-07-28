using System;
using System.Linq;

namespace FontBrowser
{
    public class Settings : Serializable<Settings>
    {
        public Settings()
        {
            FontExtensions = ".fon,.otf,.pfm,.ttc,.ttf";
        }

        public string FolderPath { get; set; }
        public string FontExtensions { get; set; }

        public bool IsFontExtension(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
                return false;

            return (FontExtensions?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)?.Any(s => string.Compare(extension, s, StringComparison.OrdinalIgnoreCase) == 0)).GetValueOrDefault();
        }
    }
}
