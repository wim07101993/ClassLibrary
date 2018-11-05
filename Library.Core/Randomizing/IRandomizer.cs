using System;
using Library.Core.Collections;

namespace Library.Core.Randomizing
{
    public interface IRandomizer
    {
        Random Random { get; }

        List<char> PossibleChars { get; }

        double NextDouble();

        int Next();
        int Next(int maxValue);
        int Next(int minValue, int maxValue);

        byte[] Next(byte[] buffer);

        string NextString(int length);
        char NextChar();
    }
}