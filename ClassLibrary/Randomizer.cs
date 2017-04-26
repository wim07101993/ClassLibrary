using System;
using System.Text;


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

        private static string NextString(int length)
        {
            var ret = "";
            var i = 0;

            while (i < length)
            {
                ret += Encoding.Default.GetString(BitConverter.GetBytes(Next()));
                i += 4;
            }

            return ret;
        }
    }
}
