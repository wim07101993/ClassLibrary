namespace DotNetCoreConsole.Parsing
{
    public class IndexedPart
    {
        public IndexType Type { get; set; }
        public int StartIndex { get; set; } = -1;
        public int EndIndex { get; set; } = -1;
    }
}
