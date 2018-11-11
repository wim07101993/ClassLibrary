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


        public Object EnterProperty(string propertyName)
        {
            _history += CurrentObject.Properties[propertyName].GetValue();
            return CurrentObject;
        }

        public Object EnterField(string fieldName)
        {
            _history += CurrentObject.Fields[fieldName].GetValue();
            return CurrentObject;
        }

        public Object EnterLocalVariable(string variableName)
        {
            _history += CurrentObject.LocalVariables[variableName];
            return CurrentObject;
        }

        public void InvokeMethod(string methodName, object[] parameters, string resultName = null)
        {
            var result = CurrentObject.Methods[methodName].Invoke(parameters);

            if (result.NoResult || string.IsNullOrWhiteSpace(resultName))
                return;

            if (CurrentObject.LocalVariables.ContainsKey(resultName))
                CurrentObject.LocalVariables[resultName] = result.Value;
            else
                CurrentObject.LocalVariables.Add(resultName, result.Value);
        }

        public Object ExitMember(int numberOfLevels = 1)
        {
            for (var i = numberOfLevels - 1; i >= 0; i--)
                _history.RemoveLast();

            return CurrentObject;
        }
    }
}