using System;
using System.IO;
using System.Windows.Media.Imaging;


namespace ClassLibrary.Windows.Extensions
{
    /// <summary>
    /// A static class with extensions for the <see cref="BitmapSource"/> class.
    /// </summary>
    public static class BitmapSourceExtensions
    {
        /// <summary>
        /// Converts this <see cref="BitmapSource"/> to a <see cref="byte"/>[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this BitmapSource image)
        {
            if (image == null) return null;

            try
            {
                byte[] data;
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (var ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }
                return data;
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        /// <summary>
        /// Saves this <see cref="BitmapSource"/> to the path: <see cref="filePath"/>.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="filePath"></param>
        public static void Save(this BitmapSource image, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        /// <summary>
        /// Converts this <see cref="BitmapImage"/> to a <see cref="BitmapSource"/>
        /// </summary>
        /// <param name="This"></param>
        /// <returns></returns>
        public static BitmapSource ToBitmapSource(this BitmapImage This)
        {
            using (var ms = new MemoryStream())
            {
                var dImg = This.ToBitmap();

                dImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                var bImg = new BitmapImage();
                bImg.BeginInit();
                bImg.StreamSource = new MemoryStream(ms.ToArray());
                bImg.EndInit();
                return bImg;
            }
        }
    }
}
