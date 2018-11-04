using System;
using System.Collections.Generic;
using System.Text;
using Shared.Serialization.Extensions;

namespace Shared.Serialization.Csv
{
    public class Header
    {
        private Type _type;


        public Header()
        {
        }

        public Header(Type type, char delimiter)
        {
            Type = type;
            Delimiter = delimiter;
        }

        public Header(Type type, char delimiter, string strHeader)
        {
            
        }


        public Type Type
        {
            get => _type;
            set
            {
                _type = value;
                Elements = _type.GetCsvHeaderElements(null, Delimiter);
            }
        }

        public char Delimiter { get; }

        public IReadOnlyList<HeaderElement> Elements { get; private set; }

        public override string ToString()
            => Enumerable.IsNullOrEmpty(Elements)
                ? base.ToString()
                : Elements.ToString("");

        public string CreateRow(object value)
        {
            using (var elementsEnumerator = Elements.GetEnumerator())
            {
                if (!elementsEnumerator.MoveNext())
                    return "";

                var rowBuilder = new StringBuilder();
                var rowElement = elementsEnumerator.Current.CreateRowElement(value);
                rowBuilder.Append($"{rowElement}{Delimiter}");

                while (elementsEnumerator.MoveNext())
                {
                    rowElement = elementsEnumerator.Current.CreateRowElement(value);
                    rowBuilder.Append(rowElement);
                }

                return rowBuilder.ToString();
            }
        }
    }
}