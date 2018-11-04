using System;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Shell;

namespace Library.WPF.Extensions
{
    public static class FileInfoExtensions
    {
        public static bool TryGetThumbnail(this FileInfo fileInfo, out BitmapSource bitmapSource,
            EThumbnailSize size = EThumbnailSize.Large)
        {
            bitmapSource = null;

            if (!ShellObject.IsPlatformSupported || fileInfo == null || !fileInfo.Exists)
                return false;

            var shellItem = ShellObject.FromParsingName(fileInfo.FullName);

            try
            {
                switch (size)
                {
                    case EThumbnailSize.Small:
                        bitmapSource = shellItem.Thumbnail.SmallBitmapSource;
                        break;
                    case EThumbnailSize.Medium:
                        bitmapSource = shellItem.Thumbnail.MediumBitmapSource;
                        break;
                    case EThumbnailSize.Large:
                        bitmapSource = shellItem.Thumbnail.LargeBitmapSource;
                        break;
                    case EThumbnailSize.ExtraLarge:
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