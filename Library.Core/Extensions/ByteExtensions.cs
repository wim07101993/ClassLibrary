using System;

namespace Library.Core.Extensions
{
    public static class ByteExtensions
    {
        public static char ToChar(this byte b)
            => Convert.ToChar(b);
    }
}