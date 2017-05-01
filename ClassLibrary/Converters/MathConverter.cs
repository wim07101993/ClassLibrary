using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;


namespace WindowsClassLibrary.Converters
{
    /// <summary>
    /// Value converter that performs arithmetic calculations over its argument(s)
    /// </summary>
    /// <remarks>
    /// MathConverter can act as a value converter, or as a multivalue converter (WPF only).
    /// It is also a markup extension (WPF only) which allows to avoid declaring resources,
    /// ConverterParameter must contain an arithmetic expression over converter arguments. Operations supported are +, -, * and /
    /// Single argument of a value converter may referred as x, a, or {0}
    /// Arguments of multi value converter may be referred as x,y,z,t (first-fourth argument), or a,b,c,d, or {0}, {1}, {2}, {3}, {4}, ...
    /// The converter supports arithmetic expressions of arbitrary complexity, including nested subexpressions
    /// </remarks>
    public class MathConverter : MarkupExtension, IMultiValueConverter, IValueConverter
    {
        readonly Dictionary<string, IExpression> _storedExpressions = new Dictionary<string, IExpression>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(new[] { value }, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var result = Parse(parameter.ToString()).Eval(values);
                if (targetType == typeof(decimal)) return result;
                if (targetType == typeof(string)) return result.ToString(CultureInfo.InvariantCulture);
                if (targetType == typeof(int)) return (int)result;
                if (targetType == typeof(double)) return (double)result;
                if (targetType == typeof(long)) return (long)result;
                throw new ArgumentException($"Unsupported target type {targetType.FullName}");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

#if !SILVERLIGHT
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
#endif
        protected virtual void ProcessException(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        private IExpression Parse(string s)
        {
            IExpression result;
            if (_storedExpressions.TryGetValue(s, out result)) return result;

            result = new Parser().Parse(s);
            _storedExpressions[s] = result;

            return result;
        }

        private interface IExpression
        {
            decimal Eval(object[] args);
        }

        private class Constant : IExpression
        {
            private readonly decimal _value;

            public Constant(string text)
            {
                if (!decimal.TryParse(text, out _value))
                {
                    throw new ArgumentException($"'{text}' is not a valid number");
                }
            }

            public decimal Eval(object[] args)
            {
                return _value;
            }
        }

        private class Variable : IExpression
        {
            private readonly int _index;

            public Variable(string text)
            {
                if (!int.TryParse(text, out _index) || _index < 0)
                {
                    throw new ArgumentException($"'{text}' is not a valid parameter index");
                }
            }

            public Variable(int n)
            {
                _index = n;
            }

            public decimal Eval(object[] args)
            {
                if (_index >= args.Length)
                {
                    throw new ArgumentException(
                        $"MathConverter: parameter index {_index} is out of range. {args.Length} parameter(s) supplied");
                }

                return System.Convert.ToDecimal(args[_index]);
            }
        }

        private class BinaryOperation : IExpression
        {
            private readonly Func<decimal, decimal, decimal> _operation;
            private readonly IExpression _left;
            private readonly IExpression _right;

            public BinaryOperation(char operation, IExpression left, IExpression right)
            {
                _left = left;
                _right = right;
                switch (operation)
                {
                    case '+': _operation = (a, b) => (a + b); break;
                    case '-': _operation = (a, b) => (a - b); break;
                    case '*': _operation = (a, b) => (a * b); break;
                    case '/': _operation = (a, b) => (a / b); break;
                    default: throw new ArgumentException("Invalid operation " + operation);
                }
            }

            public decimal Eval(object[] args)
            {
                return _operation(_left.Eval(args), _right.Eval(args));
            }
        }

        private class Negate : IExpression
        {
            private readonly IExpression _param;

            public Negate(IExpression param)
            {
                _param = param;
            }

            public decimal Eval(object[] args)
            {
                return -_param.Eval(args);
            }
        }

        private class Parser
        {
            private string _text;
            private int _pos;

            public IExpression Parse(string text)
            {
                try
                {
                    _pos = 0;
                    _text = text;
                    var result = ParseExpression();
                    RequireEndOfText();
                    return result;
                }
                catch (Exception ex)
                {
                    var msg =
                        $"MathConverter: error parsing expression '{text}'. {ex.Message} at position {_pos}";

                    throw new ArgumentException(msg, ex);
                }
            }

            private IExpression ParseExpression()
            {
                IExpression left = ParseTerm();

                while (true)
                {
                    if (_pos >= _text.Length) return left;

                    var c = _text[_pos];

                    if (c == '+' || c == '-')
                    {
                        ++_pos;
                        var right = ParseTerm();
                        left = new BinaryOperation(c, left, right);
                    }
                    else
                    {
                        return left;
                    }
                }
            }

            private IExpression ParseTerm()
            {
                var left = ParseFactor();

                while (true)
                {
                    if (_pos >= _text.Length) return left;

                    var c = _text[_pos];

                    if (c == '*' || c == '/')
                    {
                        ++_pos;
                        var right = ParseFactor();
                        left = new BinaryOperation(c, left, right);
                    }
                    else
                    {
                        return left;
                    }
                }
            }

            private IExpression ParseFactor()
            {
                SkipWhiteSpace();
                if (_pos >= _text.Length) throw new ArgumentException("Unexpected end of text");

                var c = _text[_pos];

                switch (c)
                {
                    case '+':
                        ++_pos;
                        return ParseFactor();
                    case '-':
                        ++_pos;
                        return new Negate(ParseFactor());
                    case 'x':
                    case 'a':
                        return CreateVariable(0);
                    case 'y':
                    case 'b':
                        return CreateVariable(1);
                    case 'z':
                    case 'c':
                        return CreateVariable(2);
                    case 't':
                    case 'd':
                        return CreateVariable(3);
                    case '(':
                        ++_pos;
                        var expression = ParseExpression();
                        SkipWhiteSpace();
                        Require(')');
                        SkipWhiteSpace();
                        return expression;
                    case '{':
                        ++_pos;
                        var end = _text.IndexOf('}', _pos);
                        if (end < 0) { --_pos; throw new ArgumentException("Unmatched '{'"); }
                        if (end == _pos) { throw new ArgumentException("Missing parameter index after '{'"); }
                        var result = new Variable(_text.Substring(_pos, end - _pos).Trim());
                        _pos = end + 1;
                        SkipWhiteSpace();
                        return result;
                }

                const string decimalRegEx = @"(\d+\.?\d*|\d*\.?\d+)";
                var match = Regex.Match(_text.Substring(_pos), decimalRegEx);
                if (match.Success)
                {
                    _pos += match.Length;
                    SkipWhiteSpace();
                    return new Constant(match.Value);
                }

                throw new ArgumentException($"Unexpeted character '{c}'");
            }

            private IExpression CreateVariable(int n)
            {
                ++_pos;
                SkipWhiteSpace();
                return new Variable(n);
            }

            private void SkipWhiteSpace()
            {
                while (_pos < _text.Length && char.IsWhiteSpace((_text[_pos]))) ++_pos;
            }

            private void Require(char c)
            {
                if (_pos >= _text.Length || _text[_pos] != c)
                {
                    throw new ArgumentException("Expected '" + c + "'");
                }

                ++_pos;
            }

            private void RequireEndOfText()
            {
                if (_pos != _text.Length)
                {
                    throw new ArgumentException("Unexpected character '" + _text[_pos] + "'");
                }
            }
        }
    }
}
