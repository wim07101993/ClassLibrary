using System;
using System.ComponentModel;
using System.Reflection;

namespace ClassLibrary.Extensions
{
    public static class ParameterInfoExtensions
    {
        public static string GetName(this ParameterInfo This)
        {
            return Attribute.IsDefined(This, typeof(DisplayNameAttribute))
                ? ((DisplayNameAttribute)This.GetCustomAttribute(typeof(DisplayNameAttribute))).DisplayName
                : This.Name;
        }
    }
}
