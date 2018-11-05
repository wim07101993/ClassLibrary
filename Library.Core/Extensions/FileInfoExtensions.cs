using System;
using System.IO;
using System.Threading.Tasks;

namespace Library.Core.Extensions
{
    public static class FileInfoExtensions
    {
        public static async Task CopyToAsync(this FileInfo file, FileInfo destination, Action<int> progressCallback)
        {
            const int bufferSize = 1024 * 1024; //1MB

            var buffer = new byte[bufferSize];
            var previousProgress = 0;

            using (var source = file.OpenRead())
            using (var dest = destination.OpenWrite())
            {
                dest.SetLength(source.Length);
                int read;
                for (long size = 0; size < file.Length; size += read)
                {
                    var progress = (int) (size / (double) file.Length * 100);
                    if (progress != previousProgress)
                    {
                        progressCallback?.RunAsync(progress);
                        previousProgress = progress;
                    }

                    read = source.Read(buffer, 0, bufferSize);
                    await dest.WriteAsync(buffer, 0, read);
                }
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