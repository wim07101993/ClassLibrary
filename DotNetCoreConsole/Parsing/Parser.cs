using System;
using System.Linq;
using DotNetCoreConsole.Reflected;
using Library.Core.Extensions;
using Library.Serialization;
using Object = DotNetCoreConsole.Reflected.Object;

namespace DotNetCoreConsole.Parsing
{
    public class Parser
    {
        private readonly IDeserializer _deserializer;
        private readonly ISerializer _serializer;
        private readonly INavigationService _navigationService;


        public Parser(IDeserializer deserializer, ISerializer serializer, INavigationService navigationService)
        {
            _deserializer = deserializer;
            _serializer = serializer;
            _navigationService = navigationService;
        }

        
        public object BaseObject
        {
            get => _navigationService.History.First();
            set => _navigationService.CurrentObject = new Object(value, value.GetType());
        }


        public object Parse(string s, Type type)
        {
            if (type == typeof(string))
                return s;
            if (type == typeof(char))
                return char.Parse(s);

            if (type == typeof(bool))
                return bool.Parse(s);

            if (type == typeof(byte))
                return byte.Parse(s);
            if (type == typeof(sbyte))
                return sbyte.Parse(s);
            if (type == typeof(ushort))
                return ushort.Parse(s);
            if (type == typeof(short))
                return short.Parse(s);
            if (type == typeof(uint))
                return uint.Parse(s);
            if (type == typeof(int))
                return float.Parse(s);
            if (type == typeof(ulong))
                return ulong.Parse(s);
            if (type == typeof(long))
                return long.Parse(s);
            if (type == typeof(decimal))
                return decimal.Parse(s);
            if (type == typeof(double))
                return double.Parse(s);
            if (type == typeof(float))
                return float.Parse(s);

            return _deserializer.Deserialize(s, type);
        }

        public string Interpret(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            if (CheckProgramFunctions(input, out var response))
                return response;

            input = NavigateUp(input);

            var output = InterpretInternal(input);
            return _serializer.Serialize(output.Instance);
        }

        private Object InterpretInternal(string input)
        {
           
        }

        private bool CheckProgramFunctions(string input, out string response)
        {
            if (input[0] != '\\')
            {
                response = null;
                return false;
            }

            // TODO add more program functions
            switch (input)
            {
                case "\\help":
                case "\\-h":
                    response = GenerateHelp();
                    return true;
                default:
                    response = null;
                    return false;
            }
        }

        private string GenerateHelp()
        {
            // TODO
            throw new NotImplementedException();
        }

        private string NavigateUp(string input)
        {
            input = input.Trim();
            while (input[0] == '.' && input[1] == '.' && input[2] == '/')
            {
                _navigationService.ExitMember();
                input = input.Substring(2);
            }

            return input;
        }

        private Object Set(string input)
        {
            var split = input
                .SplitOnFirst('=')
                .Select(x => x.Trim())
                .ToArray();

            var name = split[0];

            var value = Interpret(split[1]);
            var property = _navigationService
                .CurrentObject
                .Properties
                .FirstOrDefault(x => x.Key == name)
                .Value;

            if (property != null)
                property.SetValue(value.Instance);
            else
            {
                var field = _navigationService
                    .CurrentObject
                    .Fields
                    .FirstOrDefault(x => x.Key == name)
                    .Value;

                if (field != null)
                    field.SetValue(value.Instance);
                else
                    throw new NotFoundException(
                        $"There is no property or field corresponding with the name {input}");
            }

            return new Object($"Set the value of {name} to {_serializer.Serialize(value.Instance)}",
                typeof(string));
        }

        private Object Invoke(string input)
        {
            throw new NotImplementedException();
        }

        private Object Index(string input)
        {
            throw new NotImplementedException();
        }

        private Object NavigateDown(string input)
        {
            if (_navigationService.CurrentObject.Properties.ContainsKey(input))
                return _navigationService.EnterProperty(input);
            if (_navigationService.CurrentObject.Fields.ContainsKey(input))
                return _navigationService.EnterField(input);

            throw new NotFoundException($"There is no property or field corresponding with the name {input}");
        }
    }
}