using System.Linq;
using Library.Core.Collections;
using Library.Core.Extensions;

namespace DotNetCoreConsole.Parsing
{
    public class IndexedString
    {
        private readonly string _input;
   

        public IndexedString(string input)
        {
            _input = input;
            Index();
        }


        public List<IndexedPart> Parts { get; } = new List<IndexedPart>();


        private void Index()
        {
            var index = 0;
            while (index < _input.Length)
            {
                switch (_input[index])
                {
                    case '\\':
                        {
                            index++;
                            if (_input[index] == '\\')
                            {
                                Parts.Add(new IndexedPart { Type = EIndexType.BackSlash, StartIndex = index });
                                index++;
                            }

                            break;
                        }
                    case '"':
                        Parts.Add(GetString(ref index));
                        break;
                    case '\'':
                        Parts.Add(GetChar(ref index));
                        break;
                    case '(':
                        Parts.Add(new IndexedPart { Type = EIndexType.Parentheses, StartIndex = index });
                        break;
                    case ')':
                        SetClosingChar(index, EIndexType.Parentheses);
                        break;
                    case '{':
                        Parts.Add(new IndexedPart { Type = EIndexType.Braces, StartIndex = index });
                        break;
                    case '}':
                        SetClosingChar(index, EIndexType.Braces);
                        break;
                    case '[':
                        Parts.Add(new IndexedPart { Type = EIndexType.Brackets, StartIndex = index });
                        break;
                    case ']':
                        SetClosingChar(index, EIndexType.Brackets);
                        break;
                    case ',':
                        Parts.Add(new IndexedPart { Type = EIndexType.Comma, StartIndex = index });
                        break;
                    case '.':
                        Parts.Add(new IndexedPart { Type = EIndexType.Accessor, StartIndex = index });
                        break;
                    case '&':
                        Parts.Add(GetDoubleChar(ref index, '&', EIndexType.BitwiseAnd, EIndexType.And));
                        break;
                    case '|':
                        Parts.Add(GetDoubleChar(ref index, '|', EIndexType.BitwiseOr, EIndexType.Or));
                        break;
                    case '!':
                        Parts.Add(new IndexedPart { Type = EIndexType.Not, StartIndex = index });
                        break;
                    case '~':
                        Parts.Add(new IndexedPart { Type = EIndexType.BitwiseInvert, StartIndex = index });
                        break;
                    case '=':
                        Parts.Add(GetDoubleChar(ref index, '=', EIndexType.Assign, EIndexType.Equals));
                        break;
                    case '+':
                        Parts.Add(new IndexedPart { Type = EIndexType.Add, StartIndex = index });
                        break;
                    case '-':
                        Parts.Add(new IndexedPart { Type = EIndexType.Subtract, StartIndex = index });
                        break;
                    case '*':
                        Parts.Add(new IndexedPart { Type = EIndexType.Multiply, StartIndex = index });
                        break;
                    case '/':
                        Parts.Add(new IndexedPart { Type = EIndexType.Divide, StartIndex = index });
                        break;
                    case '%':
                        Parts.Add(new IndexedPart { Type = EIndexType.Modulus, StartIndex = index });
                        break;
                    case '>':
                        Parts.Add(GetDoubleChar(ref index, '>', EIndexType.GreaterThan, EIndexType.ShiftRight));
                        break;
                    case '<':
                        Parts.Add(GetDoubleChar(ref index, '<', EIndexType.LessThan, EIndexType.ShiftLeft));
                        break;
                }

                index++;
            }
        }

        private IndexedPart GetString(ref int index)
        {
            var part = new IndexedPart {Type = EIndexType.String, StartIndex = index};
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

        private IndexedPart GetChar(ref int index)
        {
            var startIndex = index;
            index++;

            if (_input[index] == '\\') // '\r'
                index++;
            index++;

            if (index < _input.Length && _input[index] == '\'')
                return new IndexedPart {Type = EIndexType.Character, StartIndex = startIndex, EndIndex = index};

            throw new NoClosingCharException('\'', _input, index);
        }

        private void SetClosingChar(int index, EIndexType indexType)
        {
            var between = Parts.GetBetween(
                    x => x.EndIndex == -1 && x.Type == indexType,
                    _ => false)
                .Where(x => x.EndIndex != -1);

            foreach (var part in between)
            {
                if (part.Type == indexType)
                {
                    part.EndIndex = index;
                    return;
                }

                switch (part.Type)
                {
                    case EIndexType.Parentheses:
                        throw new NoClosingParenthesisException(_input, part.StartIndex);
                    case EIndexType.Braces:
                        throw new NoClosingBraceException(_input, part.StartIndex);
                    case EIndexType.Brackets:
                        throw new NoClosingBracketException(_input, part.StartIndex);
                }
            }

            char c;
            switch (indexType)
            {
                case EIndexType.Parentheses:
                    c = ')';
                    break;
                case EIndexType.Braces:
                    c = '}';
                    break;
                case EIndexType.Brackets:
                    c = ']';
                    break;
                default:
                    c = default(char);
                    break;
            }

            throw new UnexpectedCharException(c, _input, index);
        }

        private IndexedPart GetDoubleChar(ref int index, char c, EIndexType ifSingle, EIndexType ifDouble)
        {
            if (index + 1 >= _input.Length || _input[index + 1] != c)
                return new IndexedPart {Type = ifSingle, StartIndex = index};

            var part = new IndexedPart {Type = ifDouble, StartIndex = index};
            index++;
            return part;
        }
    }
}