using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace FontBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var fonts = new List<Font>();
            string path = @"c:\windows\fonts";
            foreach (var ff in Fonts.GetFontFamilies(System.IO.Path.GetFullPath(path)))
            {
                foreach (var fa in ff.GetTypefaces())
                {
                    string name = fa.FaceNames[XmlLanguage.GetLanguage("en-us")];
                    var font = new Font(fa, name);
                    fonts.Add(font);
                }
            }
            LV.ItemsSource = fonts.OrderBy(f => f.Name);
        }

        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var font = e.AddedItems.OfType<Font>().FirstOrDefault();
            if (font != null)
            {
                if (font.Face.TryGetGlyphTypeface(out var face))
                {
                }
            }
        }
    }
}
