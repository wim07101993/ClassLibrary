using System;
using System.Reflection;

namespace DotNetCoreConsole.Reflected
{
    public class Field : AMemberWithValue
    {
        public Field(FieldInfo field, object parent)
            : base(field, parent)
        {
        }


        public FieldInfo FieldInfo => Info as FieldInfo;

        public override Type Type => FieldInfo.FieldType;


        public override Object GetValue() => new Object(FieldInfo.GetValue(Parent));
        public override void SetValue(object value) => FieldInfo.SetValue(Parent, value);
    }
}