namespace DotNetCoreConsole.Parsing
{
    public class NoClosingQuoteException : NoClosingCharException
    {
        public NoClosingQuoteException(string input, int index)
            : base('"', input,index)
        {
        }
    }
}