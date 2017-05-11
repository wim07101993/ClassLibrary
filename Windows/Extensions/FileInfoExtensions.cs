using System;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Shell;


namespace ClassLibrary.Windows.Extensions
{
    /// <summary>
    /// A static class with extensions for the <see cref="FileInfo"/> class.
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Enum to indicate the size of the shown bitmap.
        /// </summary>
        public enum BitmapSize
        {
            Small,
            Medium,
            Large,
            ExtraLarge
        }

        /// <summary>
        /// Gets the thumbnail of this file using the Microsoft.WindowsAPICodePack.Shell.
        /// Returns false if an error occured. The out parameter <see cref="bitmapSource"/> is in that case null.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="bitmapSource"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static bool TryGetThumbnail(this FileInfo This, out BitmapSource bitmapSource,
            BitmapSize size = BitmapSize.Large)
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
    }
}