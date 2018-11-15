namespace DotNetCoreConsole.Parsing
{
    public class NoClosingStringException : UnexpectedStringException
    {
        public NoClosingStringException(string missing, string input, int index)
        : base(missing, input, index)
        {
        }

        public override string Message
            => $"There was no closing \"{String}\" found after the opening one at index {Index}:\r\n{Input}";
    }
}