using System;
using System.Linq;
using System.Text;
using Library.Collections;
using Library.Extensions;

namespace Library.Randomizing
{
    public class Randomizer : IRandomizer
    {
        #region PROPERTIES

        public Random Random { get; } = new Random();

        public static Randomizer Instance { get; } = new Randomizer();

        public List<char> PossibleChars { get; } = new List<char>
        {
            '$', '%', '#', '@', '!', '*', '?', ';', ':', '^', '&',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
        };

        #endregion PROEPRTIES


        #region METHODS

        public double NextDouble()
            => Random.NextDouble();

        public int Next()
            => Random.Next();

        public int Next(int maxValue)
            => Random.Next(maxValue);

        public int Next(int minValue, int maxValue)
            => Random.Next(minValue, maxValue);

        public byte[] Next(byte[] buffer)
        {
            Random.NextBytes(buffer);
            return buffer;
        }

        public string NextString(int length)
        {
            var bytes = new byte[length];
            Next(bytes);

            var builder = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var c = bytes[i].ToChar();
                builder.Append(PossibleChars.Any(x => x == c) ? NextChar() : c);
            }

            return builder.ToString();
        }

        public char NextChar()
            => PossibleChars[Next(PossibleChars.Count)];

        #endregion METHODS
    }
}