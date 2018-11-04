using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public static class Enumerable
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> enumerable)
            => enumerable?.Any() == true;
    }
}