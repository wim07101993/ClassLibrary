using System;
using System.Linq;
using System.Reflection;

namespace Shared.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static bool HasAttribute<T>(this PropertyInfo property) where T : Attribute
            => property.GetCustomAttributes().Any(x => x is T);
    }
}