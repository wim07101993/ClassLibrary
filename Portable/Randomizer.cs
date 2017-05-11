﻿using System;
using System.Linq;


namespace ClassLibrary.Portable
{

    /// <summary>
    /// Static class to use an object of the class <see cref="Randomizer"/>.
    /// </summary>
    public static class Randomizer
    {
        #region PROPERTIES

        /// <summary>
        /// The instance of the Random Class
        /// </summary>
        public static Random Random { get; } = new Random();

        #endregion PROEPRTIES


        #region METHODS

        /// <summary>
        /// Returns a random floating-point number that is greater or equal than 0.0, and less than 1.0.
        /// </summary>
        /// <returns></returns>
        public static double NextDouble()
            => Random.NextDouble();

        /// <summary>
        /// Returns a non negative random integer.
        /// </summary>
        /// <returns></returns>
        public static int Next()
            => Random.Next();
        /// <summary>
        /// Returns a non negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int Next(int maxValue)
            => Random.Next(maxValue);
        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int Next(int minValue, int maxValue)
            => Random.Next(minValue, maxValue);

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer"></param>
        public static void NextByte(byte[] buffer)
            => Random.NextBytes(buffer);

        /// <summary>
        /// Returns a random string of the specified length.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string NextString(int length)
        {
            var bytes = new byte[length];
            NextByte(bytes);

            return bytes.Aggregate("", (current, b) => current + b);
        }

        #endregion METHODS
    }
}