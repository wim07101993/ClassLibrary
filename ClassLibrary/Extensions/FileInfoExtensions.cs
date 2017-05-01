using System;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Shell;


namespace WindowsClassLibrary.Extensions
{
    public static class FileInfoExtensions
    {
        public enum BitmapSize
        {
            Small,
            Medium,
            Large,
            ExtraLarge
        }

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