using System;
using System.Reflection;

namespace DotNetCoreConsole.Reflected
{
    public class Variable : AMemberWithValue
    {
        private object _value;

        public Variable(string name, object value, object parent)
            : base(null, parent)
        {
            Name = name;
            _value = value;
        }

        public override Type Type => _value.GetType();

        public override string Name { get; }

        public override Object GetValue() 
            => new Object(_value);

        public override void SetValue(object value)
        {
            var valueType = value.GetType();
            if (Type == valueType)
                _value = value;
            else
                throw new InvalidOperationException($"Cannot assign value of type {valueType.Name} to variable of type {Type.Name}");
        }

    }
}
