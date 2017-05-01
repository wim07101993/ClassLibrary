using System;
using System.ComponentModel;
using System.Reflection;


namespace PortableClassLibrary.Extensions
{
    public static class MemberInfoExtensions
    {
        public static string GetDisplayName(this MemberInfo This)
            => Attribute.IsDefined(This, typeof(DisplayNameAttribute))
                ? ((DisplayNameAttribute)This.GetCustomAttribute(typeof(DisplayNameAttribute))).DisplayName
                : This.Name;

        public static bool HasAttribute(this MemberInfo This, Type attribute)
            => Attribute.IsDefined(This, attribute);
    }
}
