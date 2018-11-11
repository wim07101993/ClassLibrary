using System;

namespace DotNetCoreConsole.Parsing
{
    public class UnexpectedCharException : Exception
    {
        public UnexpectedCharException(char character, string input, int index)
        {
            Character = character;
            Input = input;
            Index = index;
        }

        public override string Message
            => $"Unexpected character \"{Character}\" found at index {Index}:\r\n{Input}";

        public char Character { get; }
        public string Input { get; }
        public int Index { get; }
    }
}
