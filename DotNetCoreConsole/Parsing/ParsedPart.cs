namespace DotNetCoreConsole.Parsing
{
    public enum EPartType
    {
        BackSlash,      // \
        String,         // ""
        Character,      // ''
        Accessor,       // .
        Parentheses,    // ()
        Braces,         // {}
        Brackets,       // []
        Comma,          // ,
        And,            // &&
        BitwiseAnd,     // &
        Or,             // ||
        BitwiseOr,      // |
        Not,            // !
        BitwiseInvert,  // ~
        Add,            // +
        Subtract,       // -
        Multiply,       // *
        Divide,         // /
        Modulus,        // %
        GreaterThan,    // >
        LessThan,       // <
        ShiftRight,     // >>
        ShiftLeft,      // <<
    }

    public class ParsedPart
    {
        public EPartType Type { get; set; }
        public int StartIndex { get; set; } = -1;
        public int EndIndex { get; set; } = -1;
    }
}
