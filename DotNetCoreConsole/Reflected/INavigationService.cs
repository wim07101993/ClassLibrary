using System.Collections.Generic;

namespace DotNetCoreConsole.Reflected
{
    public interface INavigationService
    {
        IReadOnlyList<Object> History { get;  }
        Object CurrentObject { get; set; }


        Object EnterMemberWithValue(AMemberWithValue member);
        void InvokeMethod(Method method, object[] parameters, string resultName = null);

        Object ExitMember(int numberOfLevels = 1);
    }
}