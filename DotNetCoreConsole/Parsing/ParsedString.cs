using System.Linq;
using Library.Core.Collections;
using Library.Core.Extensions;

namespace DotNetCoreConsole.Parsing
{
    public class ParsedString
    {
        private readonly string _input;
        private readonly List<ParsedPart> _parts = new List<ParsedPart>();

        public ParsedString(string input)
        {
            _input = input;

            var index = 0;
            while (index < _input.Length)
            {
                switch (_input[index])
                {
                    case '\\':
                        index++;
                        if (_input[index] == '\\')
                        {
                            _parts.Add(new ParsedPart {Type = EPartType.BackSlash, StartIndex = index});
                            index++;
                        }

                        break;
                    case '"':
                        _parts.Add(GetString(ref index));
                        break;
                    case '\'':
                        _parts.Add(GetChar(ref index));
                        break;
                    case '(':
                        _parts.Add(new ParsedPart {Type = EPartType.Parentheses, StartIndex = index});
                        break;
                    case ')':
                        SetClosingChar(index, EPartType.Parentheses);
                        break;
                    case '{':
                        _parts.Add(new ParsedPart {Type = EPartType.Braces, StartIndex = index});
                        break;
                    case '}':
                        SetClosingChar(index, EPartType.Braces);
                        break;
                    case '[':
                        _parts.Add(new ParsedPart {Type = EPartType.Brackets, StartIndex = index});
                        break;
                    case ']':
                        SetClosingChar(index, EPartType.Brackets);
                        break;
                    case '.':
                        _parts.Add(new ParsedPart {Type = EPartType.Accessor, StartIndex = index});
                        break;
                    case ',':
                        _parts.Add(new ParsedPart {Type = EPartType.Comma, StartIndex = index});
                        break;
                    case '&':
                        _parts.Add(GetDoubleChar(ref index, '&', EPartType.BitwiseAnd, EPartType.And));
                        break;
                    case '|':
                        _parts.Add(GetDoubleChar(ref index, '|', EPartType.BitwiseOr, EPartType.Or));
                        break;
                    case '!':
                        _parts.Add(new ParsedPart {Type = EPartType.Not, StartIndex = index});
                        break;
                    case '~':
                        _parts.Add(new ParsedPart {Type = EPartType.BitwiseInvert, StartIndex = index});
                        break;
                    case '+':
                        _parts.Add(new ParsedPart {Type = EPartType.Add, StartIndex = index});
                        break;
                    case '-':
                        _parts.Add(new ParsedPart {Type = EPartType.Subtract, StartIndex = index});
                        break;
                    case '*':
                        _parts.Add(new ParsedPart {Type = EPartType.Multiply, StartIndex = index});
                        break;
                    case '/':
                        _parts.Add(new ParsedPart {Type = EPartType.Divide, StartIndex = index});
                        break;
                    case '%':
                        _parts.Add(new ParsedPart {Type = EPartType.Modulus, StartIndex = index});
                        break;
                    case '>':
                        _parts.Add(GetDoubleChar(ref index, '>', EPartType.GreaterThan, EPartType.ShiftRight));
                        break;
                    case '<':
                        _parts.Add(GetDoubleChar(ref index, '<', EPartType.LessThan, EPartType.ShiftLeft));
                        break;
                }

                index++;
            }
        }

        private ParsedPart GetString(ref int index)
        {
            var part = new ParsedPart {Type = EPartType.String, StartIndex = index};
            while (index < _input.Length)
            {
                index++;
                switch (_input[index])
                {
                    case '\\':
                        index++;
                        break;
                    case '"':
                        part.EndIndex = index;
                        return part;
                }
            }

            if (part.EndIndex == -1)
                throw new NoClosingQuoteException(_input, part.StartIndex);
            return part;
        }

        private ParsedPart GetChar(ref int index)
        {
            var startIndex = index;
            index++;

            if (_input[index] == '\\') // '\r'
                index++;
            index++;

            if (index < _input.Length && _input[index] == '\'')
                return new ParsedPart {Type = EPartType.Character, StartIndex = startIndex, EndIndex = index};

            throw new NoClosingCharException('\'', _input, index);
        }

        private void SetClosingChar(int index, EPartType partType)
        {
            var between = _parts.GetBetween(
                    x => x.EndIndex == -1 && x.Type == partType,
                    _ => false)
                .Where(x => x.EndIndex != -1);

            foreach (var part in between)
            {
                if (part.Type == partType)
                {
                    part.EndIndex = index;
                    return;
                }

                switch (part.Type)
                {
                    case EPartType.Parentheses:
                        throw new NoClosingParenthesisException(_input, part.StartIndex);
                    case EPartType.Braces:
                        throw new NoClosingBraceException(_input, part.StartIndex);
                    case EPartType.Brackets:
                        throw new NoClosingBracketException(_input, part.StartIndex);
                }
            }

            char c;
            switch (partType)
            {
                case EPartType.Parentheses:
                    c = ')';
                    break;
                case EPartType.Braces:
                    c = '}';
                    break;
                case EPartType.Brackets:
                    c = ']';
                    break;
                default:
                    c = default(char);
                    break;
            }

            throw new UnexpectedCharException(c, _input, index);
        }

        private ParsedPart GetDoubleChar(ref int index, char c, EPartType ifSingle, EPartType ifDouble)
        {
            if (index + 1 >= _input.Length || _input[index + 1] != c)
                return new ParsedPart {Type = ifSingle, StartIndex = index};

            var part = new ParsedPart {Type = ifDouble, StartIndex = index};
            index++;
            return part;
        }
    }
}