using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Shared.Serialization.Extensions;

namespace Shared.Serialization.Csv
{
    public class HeaderElement
    {
        public HeaderElement(PropertyInfo property, string parentName, char delimiter)
        {
            PropertyInfo = property;
            Delimiter = delimiter;

            Name = string.IsNullOrWhiteSpace(parentName)
                ? property.Name
                : $"{parentName}.{property.Name}";

            Children = Type.GetCsvHeaderElements(Name, delimiter);
        }

        public PropertyInfo PropertyInfo { get; }
        public Type Type => PropertyInfo.PropertyType;
        public string Name { get; }

        public char Delimiter { get; }

        public IReadOnlyList<HeaderElement> Children { get; }

        
        public override string ToString()
            => Enumerable.IsNullOrEmpty(Children)
                ? Name
                : Children.ToString(Delimiter.ToString());

        public string CreateRowElement(object obj)
        {
            if (Enumerable.IsNullOrEmpty(Children))
                return PropertyInfo.GetValue(obj).ToString();
            
            using (var childrenEnumerator = Children.GetEnumerator())
            {
                var rowBuilder = new StringBuilder();
                
                childrenEnumerator.MoveNext();
                
                var rowElement = childrenEnumerator.Current.CreateRowElement(obj);
                rowBuilder.Append($"{rowElement}{Delimiter}");

                while (childrenEnumerator.MoveNext())
                {
                    rowElement = childrenEnumerator.Current.CreateRowElement(obj);
                    rowBuilder.Append(rowElement);
                }

                return rowBuilder.ToString();
            }
        }
    }
}