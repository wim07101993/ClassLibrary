namespace DotNetCoreConsole.Parsing
{
    public class IndexedPart
    {
        public IndexedPart(IndexType type, int startIndex = -1, int endIndex = -1)
        {
            Type = type;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        public IndexType Type { get; }
        public int StartIndex { get; }
        public int EndIndex { get; }
    }
}