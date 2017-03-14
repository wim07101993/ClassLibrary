using System;

namespace ClassLibrary
{
    public static class Randomizer
    {
        #region PROPERTIES

        public static Random Random { get; } = new Random();

        #endregion PROEPRTIES

        public static double NextDouble()
        {
            return Random.NextDouble();
        }

        public static int Next()
        {
            return Random.Next();
        }
        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }
        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        public static void NextByte(byte[] buffer)
        {
            Random.NextBytes(buffer);
        }
    }
}
