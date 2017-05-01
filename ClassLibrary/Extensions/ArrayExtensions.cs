using System.IO;
using System.Windows.Media.Imaging;


namespace WindowsClassLibrary.Extensions
{
    public static class ArrayExtensions
    {
        public static BitmapSource ToBitmapSource(this byte[] This)
        {
            var image = new BitmapImage();

            using (var ms = new MemoryStream(This))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
            }

            return image;
        }
    }
}
