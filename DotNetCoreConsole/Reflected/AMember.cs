using System.Reflection;

namespace DotNetCoreConsole.Reflected
{
    public abstract class AMember
    {
        protected AMember(MemberInfo member, object parent)
        {
            Parent = parent;
            Info = member;
        }


        public object Parent { get; }

        public MemberInfo Info { get; }

        public virtual string Name => Info.Name;
    }
}