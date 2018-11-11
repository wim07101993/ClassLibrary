namespace DotNetCoreConsole.Parsing
{
    public class NoClosingBracketException : NoClosingCharException
    {
        public NoClosingBracketException(string input, int index) 
            : base('[', input, index)
        {
        }
    }
}
