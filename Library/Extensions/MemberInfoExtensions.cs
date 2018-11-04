using System;
using System.ComponentModel;
using System.Reflection;

namespace Library.Extensions
{
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Gets the display name of a member defined by the <see cref="DisplayNameAttribute"/>.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static string GetDisplayName(this MemberInfo member)
            => Attribute.IsDefined(member, typeof(DisplayNameAttribute))
                ? ((DisplayNameAttribute) member.GetCustomAttribute(typeof(DisplayNameAttribute))).DisplayName
                : member.Name;

        /// <summary>
        /// Checks if a member holds the attribute <see cref="attribute"/>
        /// </summary>
        /// <param name="member"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static bool HasAttribute(this MemberInfo member, Type attribute)
            => Attribute.IsDefined(member, attribute);

        /// <summary>
        /// Checks if a member holds the attribute <see cref="T"/>
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this MemberInfo member)
            => Attribute.IsDefined(member, typeof(T));
    }
}