using System;

namespace DotNetCoreConsole.Parsing
{
    public class UnexpectedStringException : Exception
    {
        public UnexpectedStringException(string s, string input, int index)
        {
            String = s;
            Input = input;
            Index = index;
        }

        public override string Message
            => $"Unexpected string \"{String}\" found at index {Index}:\r\n{Input}";

        public string String { get; }
        public string Input { get; }
        public int Index { get; }
    }
}
