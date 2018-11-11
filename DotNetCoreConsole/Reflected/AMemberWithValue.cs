using System;
using System.Reflection;

namespace DotNetCoreConsole.Reflected
{
    public abstract class AMemberWithValue : AMember
    {
        protected AMemberWithValue(MemberInfo member, object parent)
            : base(member, parent)
        {
        }


        public abstract Type Type { get; }

        public abstract Object GetValue();
        public abstract void SetValue(object value);
    }
}