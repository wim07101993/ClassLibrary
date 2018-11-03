using System;

namespace Shared.Extensions
{
    public static class ByteExtensions
    {
        public static char ToChar(this byte b)
            => Convert.ToChar(b);
    }
}