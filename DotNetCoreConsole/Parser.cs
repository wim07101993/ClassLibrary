using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCoreConsole.Parsing;
using DotNetCoreConsole.Reflected;
using Library.Serialization;
using Object = DotNetCoreConsole.Reflected.Object;

namespace DotNetCoreConsole
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
            set => _navigationService.CurrentObject = new Object(value);
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

            var indexedString = new IndexedString(input);

            var output = InterpretInternal(input);
            return _serializer.Serialize(output.Instance);
        }

        private Object InterpretInternal(string input)
        {
            var member = _navigationService.CurrentObject[input];
            switch (member)
            {
                case AMemberWithValue memberWithValue:
                    return memberWithValue.GetValue();
                case Method method:
                    return method.Invoke(null).Value;
            }

            throw new NotFoundException($"There is no member corresponding with the name {input}");
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
                case "\\ls":
                case "\\dir":
                    response = GenerateMemberList();
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

        private string GenerateMemberList(bool includePrivate = false)
        {
            var current = _navigationService.CurrentObject;
            var builder = new StringBuilder();

            builder.AppendLine("PROPERTIES:");
            builder.AppendLine(GenerateAMemberWithValues(current.Properties.Values));

            builder.AppendLine("FIELDS:");
            builder.AppendLine(GenerateAMemberWithValues(current.Fields.Values));

            builder.AppendLine("VARIABLES:");
            builder.AppendLine(GenerateAMemberWithValues(current.LocalVariables.Values));

            builder.AppendLine("METHODS:");
            foreach (var method in _navigationService.CurrentObject.Methods.Values)
            {
                if (_navigationService.CurrentObject.Properties.Keys.Any(x => $"get_{x}" == method.Name || $"set_{x}" == method.Name))
                    continue;

                var param = method.Parameters.Values.Aggregate(new StringBuilder(), (x, y) => x.Append($"{y.Name}: {y.Type.Name}, "));
                builder.AppendLine($"{method.Name}\t\t{param}");
            }
            builder.AppendLine();

            return builder.ToString();
        }

        private string GenerateAMemberWithValues(IEnumerable<AMemberWithValue> members, bool includePrivate = false)
        {
            var builder = new StringBuilder();

            foreach (var member in members)
            {
                builder.Append(member.Name);
                builder.Append('\t');

                var tabLength = 2 - member.Name.Length / 8;
                for (var i = tabLength - 1; i >= 0; i--)
                    builder.Append('\t');

                builder.AppendLine(member.Type.Name);
            }
          
            return builder.ToString();
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
        
        private Object NavigateDown(string input)
        {
            if (_navigationService.CurrentObject[input] is AMemberWithValue memberWithValue)
                return _navigationService.EnterMemberWithValue(memberWithValue);

            throw new NotFoundException($"There is no property, field or variable corresponding with the name {input}");
        }
    }
}