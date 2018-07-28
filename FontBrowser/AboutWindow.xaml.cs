using System;
using System.Reflection;
using System.Windows;

namespace FontBrowser
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            var v = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            Header.Text = "FontBrowser V" + v + Environment.NewLine +
                "Copyright (C) 2017-" + DateTime.Now.Year +
                " Simon Mourier" + Environment.NewLine +
                "All rights reserved.";
        }

        private void Button_Click(object sender, RoutedEventArgs e) => "http://stackoverflow.com/users/403671/simon-mourier?tab=profile".OpenInDefaultBrowser();
    }
}
