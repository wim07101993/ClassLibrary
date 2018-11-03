using System;

namespace Shared.Extensions
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
    }
}