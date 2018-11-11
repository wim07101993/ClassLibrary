using System;
using System.Reflection;

namespace DotNetCoreConsole.Reflected
{
    public class Property : AMemberWithValue
    {
        public Property(PropertyInfo property, object parent)
            : base(property, parent)
        {
        }


        public PropertyInfo PropertyInfo => Info as PropertyInfo;

        public override Type Type => PropertyInfo.PropertyType;


        public override Object GetValue() => new Object(PropertyInfo.GetValue(Parent), Type);
        public override void SetValue(object value) => PropertyInfo.SetValue(Parent, value);
    }
}