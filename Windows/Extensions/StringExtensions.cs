using System;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Shell;

namespace ClassLibrary.Windows.Extensions
{
    public static class StringExtensions
    {
        public static BitmapSource GetThumbnail(this string This)
        {
            try
            {
                if (!ShellObject.IsPlatformSupported)
                    return FailSafeThumbnail(This);

                using (var shellitem = ShellObject.FromParsingName(This))
                {
                    return shellitem.Thumbnail.LargeBitmapSource;
                }
            }
            catch (Exception)
            {
                return FailSafeThumbnail(This);
            }
        }

        private static BitmapSource FailSafeThumbnail(this string This)
        {
            var bmi = new BitmapImage();
            bmi.BeginInit();
            bmi.DecodePixelWidth = 200;
            bmi.UriSource = new Uri(This);
            bmi.EndInit();
            bmi.Freeze();
            return bmi;
        }
    }
}
