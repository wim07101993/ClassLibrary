using System;
using Library.Core.Extensions;
using Newtonsoft.Json;

namespace DotNetCoreConsole.Parsing
{
    public struct IndexType : IEquatable<IndexType>
    {
        [JsonConstructor]
        public IndexType(string opener, string closer, bool useEscape, string escapeString, string splitter)
        {
            Opener = opener;
            Closer = closer;
            Splitter = splitter;

            UseEscape = useEscape;
            if (UseEscape && escapeString == null)
                throw new ArgumentNullException(nameof(escapeString));
            EscapeString = escapeString;
        }

        public IndexType(string opener, string closer, bool useEscape = false, string escapeString = "\\")
            : this(opener, closer, useEscape, escapeString, null)
        {
        }

        public IndexType(string splitter)
            : this(null, null, false, null, splitter)
        {
        }

        public string Opener { get; }
        public string Closer { get; }
        public string Splitter { get; }
        public bool UseEscape { get; }
        public string EscapeString { get; }

        public bool Equals(IndexType other)
            => Opener == other.Opener && Closer == other.Closer && Splitter == other.Splitter;

        public bool IsAtIndex(string input, int index)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return input.ContainsStringAtIndex(index, Opener) || input.ContainsStringAtIndex(index, Splitter);
        }

        public IndexedPart GetIndexedPart(string input, ref int index)
        {
            if (!IsAtIndex(input, index))
                return null;

            var startIndex = index;

            if (string.IsNullOrEmpty(Closer))
                return new IndexedPart(this, startIndex);

            for (; index < input.Length; index++)
                if (input.ContainsStringAtIndex(index, Closer))
                    return new IndexedPart(this, startIndex, index);

            throw new NoClosingStringException(Closer, input, startIndex);
        }

        
    }
}