namespace DotNetCoreConsole.Parsing
{
    public class NoClosingCharException : UnexpectedCharException
    {
        public NoClosingCharException(char missing, string input, int index)
        : base(missing, input, index)
        {
        }

        public override string Message
            => $"There was no closing \"{Character}\" found after the opening one at index {Index}:\r\n{Input}";
    }
}