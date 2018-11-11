using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotNetCoreConsole.Reflected
{
    public class Method : AMember
    {
        public Method(MethodInfo method, object parent)
            : base(method, parent)
        {
        }


        public MethodInfo MethodInfo => Info as MethodInfo;

        public Dictionary<string, Parameter> Parameters
            => MethodInfo
                .GetParameters()
                .ToDictionary(x => x.Name, x => new Parameter(x));


        public MethodResult Invoke(object[] parameters)
        {
            var result = MethodInfo.Invoke(Parent, parameters);
            return result == null
                ? new MethodResult {NoResult = true}
                : new MethodResult {Value = new Object(result, MethodInfo.ReturnType)};
        }
    }
}