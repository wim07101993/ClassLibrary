using System.IO;
using System.Windows.Media.Imaging;


namespace WindowsClassLibrary.Extensions
{
    /// <summary>
    /// A static class with extension methods for the Array class.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Converts a <see cref="byte"/>[] to a <see cref="BitmapSource"/>
        /// </summary>
        /// <param name="This"></param>
        /// <returns></returns>
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
