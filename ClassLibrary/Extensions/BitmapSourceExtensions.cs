using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;


namespace WindowsClassLibrary.Extensions
{
    public static class BitmapSourceExtensions
    {
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

        public static void Save(this BitmapSource image, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        public static BitmapSource ToBitmapSource(this BitmapImage This)
        {
            using (var ms = new MemoryStream())
            {
                Bitmap dImg = This.ToBitmap();

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
