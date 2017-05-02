using System;
using System.ComponentModel;
using System.Reflection;


namespace PortableClassLibrary.Extensions
{
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Gets the display name of a member defined by the <see cref="DisplayNameAttribute"/>.
        /// </summary>
        /// <param name="This"></param>
        /// <returns></returns>
        public static string GetDisplayName(this MemberInfo This)
            => Attribute.IsDefined(This, typeof(DisplayNameAttribute))
                ? ((DisplayNameAttribute)This.GetCustomAttribute(typeof(DisplayNameAttribute))).DisplayName
                : This.Name;

        /// <summary>
        /// Checks if a member holds the attribute <see cref="attribute"/>
        /// </summary>
        /// <param name="This"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static bool HasAttribute(this MemberInfo This, Type attribute)
            => Attribute.IsDefined(This, attribute);
    }
}
