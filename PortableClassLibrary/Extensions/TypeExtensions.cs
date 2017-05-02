using System;


namespace PortableClassLibrary.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks if this type is Nullable or the underlying type is.
        /// </summary>
        /// <param name="This"></param>
        /// <returns></returns>
        public static bool IsNullable(this Type This) 
            => !This.IsValueType || Nullable.GetUnderlyingType(This) != null;
    }
}
