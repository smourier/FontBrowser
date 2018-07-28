using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.Win32;

namespace FontBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadFonts(null);
        }

        private void LoadFonts(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            }

            var fonts = new List<Font>();
            foreach (var ff in Fonts.GetFontFamilies(System.IO.Path.GetFullPath(path)))
            {
                foreach (var fa in ff.GetTypefaces())
                {
                    if (fa.TryGetGlyphTypeface(out var gf))
                    {
                        string name = fa.FaceNames[XmlLanguage.GetLanguage("en-us")];
                        var font = new Font(fa, name, gf);
                        fonts.Add(font);
                    }
                }
            }
            LB.ItemsSource = fonts.OrderBy(f => f.Name);
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();
        private void OpenSystem_Click(object sender, RoutedEventArgs e) => LoadFonts(null);
        private void About_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AboutWindow();
            dlg.Owner = this;
            dlg.ShowDialog();
        }

        private void LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var font = e.AddedItems.OfType<Font>().FirstOrDefault();
            FNT.DataContext = font; // can be null
        }

        private void OpenLocation_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (!string.IsNullOrWhiteSpace(App.Settings.FolderPath))
            {
                dlg.InitialDirectory = App.Settings.FolderPath;
            }

            if (!dlg.ShowDialog(this).GetValueOrDefault())
                return;

            if (App.Settings.IsFontExtension(Path.GetExtension(dlg.FileName)))
            {
                LoadFonts(dlg.FileName);
            }
            else
            {
                LoadFonts(Path.GetDirectoryName(dlg.FileName));
            }
        }

        private void LBG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var glyph = ((e.OriginalSource as FrameworkElement).DataContext as Glyph);
            if (glyph == null)
                return;

            Clipboard.SetText(glyph.CodePointText);
            MessageBox.Show("'" + glyph.CodePointText + "' was copied to the clipboard.", "Font Browser");
        }
    }
}
