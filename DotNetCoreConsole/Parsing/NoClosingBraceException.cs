namespace DotNetCoreConsole.Parsing
{
    public class NoClosingBraceException : NoClosingCharException
    {
        public NoClosingBraceException(string input, int index)
            : base('{', input, index)
        {
        }
    }
}
