using System;
using System.ComponentModel;
using System.Reflection;

namespace ClassLibrary.Extensions
{
    public static class MemberInfoExtensions
    {
        public static string GetDisplayName(this MemberInfo This)
        {
            return Attribute.IsDefined(This, typeof(DisplayNameAttribute))
                ? ((DisplayNameAttribute)This.GetCustomAttribute(typeof(DisplayNameAttribute))).DisplayName
                : This.Name;
        }
    }
}
