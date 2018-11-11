namespace DotNetCoreConsole.Parsing
{
    public class NoClosingParenthesisException : NoClosingCharException
    {
        public NoClosingParenthesisException(string input, int index) 
            : base('(', input, index)
        {
        }
    }
}
