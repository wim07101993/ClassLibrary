using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNullable(this Type This)
        {
            return !This.IsValueType || (Nullable.GetUnderlyingType(This) != null);
        }
    }
}
