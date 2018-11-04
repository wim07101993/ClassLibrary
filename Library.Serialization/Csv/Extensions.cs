using System;
using System.Collections.Generic;
using System.Linq;
using Library.Extensions;

namespace Library.Serialization.Csv
{
    internal static class Extensions
    {
        public static IReadOnlyList<HeaderElement> GetCsvHeaderElements(this Type type, string parentName, char delimiter)
            => type.GetProperties()
                .Where(x => x.CanRead && x.CanWrite && !x.HasAttribute<CsvIgnore>())
                .Select(x => new HeaderElement(x, parentName, delimiter))
                .ToReadOnlyList();
    }
}