using System;
using System.Diagnostics;

namespace FontBrowser
{
    public static class Extensions
    {
        public static void OpenInDefaultBrowser(this string url)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            var psi = new ProcessStartInfo(url);
            Process.Start(psi);
        }
    }
}
