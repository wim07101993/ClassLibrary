using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Library.Core.Extensions;

namespace DotNetCoreConsole.Parsing
{
    public class IndexedString
    {
        private readonly string _input;

        private static readonly IReadOnlyList<IndexType> IndexTypes = new ReadOnlyCollection<IndexType>(
            new[]
            {
                new IndexType("\"", "\""),
                new IndexType("'", "'"),
                new IndexType("(", ")"),
                new IndexType("{", "}"),
                new IndexType("[", "]"),
                new IndexType(","),
                new IndexType("."),
                new IndexType("&&"),
                new IndexType("||"),
                new IndexType("=="),
                new IndexType("&"),
                new IndexType("|"),
                new IndexType("!"),
                new IndexType("~"),
                new IndexType("="),
                new IndexType("+"),
                new IndexType("-"),
                new IndexType("*"),
                new IndexType("/"),
                new IndexType("%"),
                new IndexType(">>"),
                new IndexType("<<"),
                new IndexType(">"),
                new IndexType("<"),
                new IndexType("^"),
            });


        public IndexedString(string input)
        {
            _input = input;
            Index();
        }


        public Library.Core.Collections.List<IndexedPart> Parts { get; } = new Library.Core.Collections.List<IndexedPart>();


        private void Index()
        {
            var index = 0;
            while (index < _input.Length)
            {
                var indexType = IndexTypes.FirstOrDefault(x => x.IsAtIndex(_input, index));
                Parts.Add(indexType.GetIndexedPart(_input, ref index));
                index++;
            }
        }
    }
}