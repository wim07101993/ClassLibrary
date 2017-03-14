using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Extensions
{
    public static class FileInfoExtensions
    {
        public enum BitmapSize
        {
            Small, Medium, Large, ExtraLarge
        }

        public static bool TryGetThumbnail(this FileInfo This, out BitmapSource bitmapSource, BitmapSize size = BitmapSize.Large)
        {
            bitmapSource = null;

            if (!ShellObject.IsPlatformSupported ||
                This == null || !This.Exists)
                return false;

            var shellItem = ShellObject.FromParsingName(This.FullName);

            try
            {
                switch (size)
                {
                    case BitmapSize.Small:
                        bitmapSource = shellItem.Thumbnail.SmallBitmapSource;
                        break;
                    case BitmapSize.Medium:
                        bitmapSource = shellItem.Thumbnail.MediumBitmapSource;
                        break;
                    case BitmapSize.Large:
                        bitmapSource = shellItem.Thumbnail.LargeBitmapSource;
                        break;
                    case BitmapSize.ExtraLarge:
                        bitmapSource = shellItem.Thumbnail.ExtraLargeBitmapSource;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(size), size, null);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void CopyTo(this FileInfo file, FileInfo destination, Action<int> progressCallback)
        {
            const int bufferSize = 1024 * 1024;  //1MB
            byte[] buffer = new byte[bufferSize], buffer2 = new byte[bufferSize];
            var swap = false;
            var reportedProgress = 0;
            var len = file.Length;
            float flen = len;
            Task writer = null;

            using (var source = file.OpenRead())
            using (var dest = destination.OpenWrite())
            {
                dest.SetLength(source.Length);
                int read;
                for (long size = 0; size < len; size += read)
                {
                    int progress;
                    if ((progress = (int)(size / flen * 100)) != reportedProgress)
                        progressCallback?.Invoke(reportedProgress = progress);
                    read = source.Read(swap ? buffer : buffer2, 0, bufferSize);
                    writer?.Wait();
                    writer = dest.WriteAsync(swap ? buffer : buffer2, 0, read);
                    swap = !swap;
                }
                writer?.Wait();
            }
        }

        public static string GetFileSizeAsString(this FileInfo file)
        {
            double fileSize = file.Length;
            if (fileSize < 1000)
                return $"{fileSize:0}B";

            fileSize = fileSize / 1024;
            if (fileSize < 1000)
                return $"{fileSize:0.#}KB";

            fileSize = fileSize / 1024;
            if (fileSize < 1000)
                return $"{fileSize:0.##}MB";

            fileSize = fileSize / 1024;
            if (fileSize < 1000)
                return $"{fileSize:0.##}GB";

            fileSize = fileSize / 1024;

            return $"{fileSize:0.##}TB";
        }
    }
}
