using System;
using System.Reflection;

namespace DotNetCoreConsole.Reflected
{
    public class Parameter
    {
        public Parameter(ParameterInfo info)
        {
            Info = info;
        }

        public ParameterInfo Info { get; }

        public string Name => Info.Name;
        public Type Type => Info.ParameterType;
    }
}