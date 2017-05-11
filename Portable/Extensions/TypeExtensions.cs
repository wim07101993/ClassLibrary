using System;


namespace ClassLibrary.Portable.Extensions
{
    /// <summary>
    /// A static class with extensions for the <see cref="Type"/> class.
    /// </summary>
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
