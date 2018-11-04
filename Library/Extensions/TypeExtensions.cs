using System;

namespace Library.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNumericType(this Type type)
            => type == typeof(byte) || type == typeof(sbyte)
                                    || type == typeof(ushort) || type == typeof(short)
                                    || type == typeof(uint) || type == typeof(int)
                                    || type == typeof(ulong) || type == typeof(long)
                                    || type == typeof(decimal) || type == typeof(double) || type == typeof(float);

        public static bool IsCharacterBasedType(this Type type)
            => type == typeof(string) || type == typeof(char);

        public static bool IsTimeType(this Type type)
            => type == typeof(DateTime) || type == typeof(TimeSpan);
        
        /// <summary>
        /// Checks if this type is Nullable or the underlying type is.
        /// </summary>
        /// <param name="This"></param>
        /// <returns></returns>
        public static bool IsNullable(this Type This) 
            => !This.IsValueType || Nullable.GetUnderlyingType(This) != null;
    }
}