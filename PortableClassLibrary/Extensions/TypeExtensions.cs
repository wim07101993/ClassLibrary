using System;


namespace PortableClassLibrary.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNullable(this Type This)
        {
            return !This.IsValueType || (Nullable.GetUnderlyingType(This) != null);
        }
    }
}
