using System.Collections.Generic;

namespace DotNetCoreConsole.Reflected
{
    public interface INavigationService
    {
        IReadOnlyList<Object> History { get;  }
        Object CurrentObject { get; set; }


        Object EnterProperty(string propertyName);
        Object EnterField(string fieldName);
        Object EnterLocalVariable(string variableName);

        void InvokeMethod(string methodName, object[] parameters, string resultName = null);

        Object ExitMember(int numberOfLevels = 1);
    }
}