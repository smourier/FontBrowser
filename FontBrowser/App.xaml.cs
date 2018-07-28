using System.Windows;

namespace FontBrowser
{
    public partial class App : Application
    {
        public static readonly Settings Settings = Settings.DeserializeFromConfiguration();
    }
}
