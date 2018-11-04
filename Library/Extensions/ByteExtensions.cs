using System;

namespace Library.Extensions
{
    public static class ByteExtensions
    {
        public static char ToChar(this byte b)
            => Convert.ToChar(b);
    }
}