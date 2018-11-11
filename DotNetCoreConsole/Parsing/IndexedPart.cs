namespace DotNetCoreConsole.Parsing
{
    public enum EIndexType
    {
        BackSlash,          // \
        String,             // ""
        Character,          // ''
        Parentheses,        // ()
        Braces,             // {}
        Brackets,           // []
        Comma,              // ,
        Accessor,           // .
        And,                // &&
        BitwiseAnd,         // &
        Or,                 // ||
        BitwiseOr,          // |
        Not,                // !
        BitwiseInvert,      // ~
        Equals,             // ==
        Assign,             // =
        Add,                // +
        Subtract,           // -
        Multiply,           // *
        Divide,             // /
        Modulus,            // %
        GreaterThan,        // >
        LessThan,           // <
        ShiftRight,         // >>
        ShiftLeft,          // <<
    }

    public class IndexedPart
    {
        public EIndexType Type { get; set; }
        public int StartIndex { get; set; } = -1;
        public int EndIndex { get; set; } = -1;
    }
}
