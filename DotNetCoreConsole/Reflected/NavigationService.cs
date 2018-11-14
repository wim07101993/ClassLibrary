using System.Linq;
using Library.Core.Collections;
using Library.Core.Extensions;

namespace DotNetCoreConsole.Reflected
{
    public class NavigationService : INavigationService
    {
        private List<Object> _history = new List<Object>();


        public System.Collections.Generic.IReadOnlyList<Object> History => _history;

        public Object CurrentObject
        {
            get => History.Last();
            set
            {
                _history.Clear();
                _history += value;
            }
        }


        public Object EnterMemberWithValue(AMemberWithValue member)
        {
            _history += member.GetValue();
            return CurrentObject;
        }
        
        public void InvokeMethod(Method method, object[] parameters, string resultName = null)
        {
            var result = method.Invoke(parameters);

            if (result.NoResult || string.IsNullOrWhiteSpace(resultName))
                return;

            if (CurrentObject.LocalVariables.ContainsKey(resultName))
                CurrentObject.LocalVariables[resultName].SetValue(result.Value);
            else
                CurrentObject.LocalVariables.Add(resultName, new Variable(resultName, result.Value, CurrentObject));
        }

        public Object ExitMember(int numberOfLevels = 1)
        {
            for (var i = numberOfLevels - 1; i >= 0; i--)
                _history.RemoveLast();

            return CurrentObject;
        }
    }
}