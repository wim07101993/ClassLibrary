using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;


namespace ClassLibrary.Windows.Extensions
{
    /// <summary>
    /// A class with extensions for the <see cref="Bitmap"/> class.
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        /// Converts a <see cref="BitmapSource"/> to a <see cref="Bitmap"/>
        /// </summary>
        /// <param name="bitmapsource"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap(this BitmapSource bitmapsource)
        {
            using (var stream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(stream);

                using (var tempBitmap = new Bitmap(stream))
                {
                    // According to MSDN, one "must keep the stream open for the lifetime of the Bitmap."
                    // So we return a copy of the new bitmap, allowing us to dispose both the bitmap and the stream.
                    return new Bitmap(tempBitmap);
                }
            }
        }

        /// <summary>
        /// Converts a <see cref="Bitmap"/> to a <see cref="BitmapSource"/>
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapSource ToBitmapSource(this Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);

                stream.Position = 0;
                var result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
    }
}
