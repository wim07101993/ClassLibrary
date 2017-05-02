﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace PortableClassLibrary.Extensions
{
    /// <summary>
    /// A static class with extensions for the <see cref="IList"/> class.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Searches for the index of the element where the <see cref="Predicate{T}"/> match, gives true.
        /// If there were no matches, -1 is returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static int FindIndex<T>(this IList<T> This, Predicate<T> match)
        {
            for (var i = 0; i < This.Count; i++)
                if (match(This[i]))
                    return i;

            return -1;
        }

        /// <summary>
        /// foreach item in the <see cref="IEnumerable"/> <see cref="itemsToAdd"/>, that item is added to this list.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="itemsToAdd"></param>
        public static void AddRange(this IList This, IEnumerable itemsToAdd)
        {
            foreach (var item in itemsToAdd)
                This.Add(item);
        }

        /// <summary>
        /// foreach item in the <see cref="IEnumerable"/> <see cref="itemsToRemove"/>, that item is removed from this list.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="itemsToRemove"></param>
        public static void RemoveRange(this IList This, IEnumerable itemsToRemove)
        {
            foreach (var item in itemsToRemove)
                This.Remove(item);
        }

        /// <summary>
        /// foreach index in the <see cref="IEnumerable"/> <see cref="indexesToRemoveAt"/>, the element at that index is removed from this list.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="indexesToRemoveAt"></param>
        public static void RemoveRange(this IList This, IEnumerable<int> indexesToRemoveAt)
        {
            foreach (var i in indexesToRemoveAt)
                This.RemoveAt(i);
        }

        /// <summary>
        /// Removes the last element from the list.
        /// </summary>
        /// <param name="This"></param>
        public static void RemoveLast(this IList This) => This.RemoveAt(This.Count - 1);
        /// <summary>
        ///  Removes the first element from the list.
        /// </summary>
        /// <param name="This"></param>
        public static void RemoveFirst(this IList This) => This.RemoveAt(0);

        /// <summary>
        /// Shuffles this List by switching each element with a random element from the list.
        /// </summary>
        /// <param name="This"></param>
        public static void Shuffle(this IList This)
        {
            for (var i = This.Count - 1; i > 1; i--)
            {
                var r = Randomizer.Next(i + 1);
                var value = This[r];
                This[r] = This[i];
                This[i] = value;
            }
        }

        /// <summary>
        /// Returns a duplicate of this list. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <returns></returns>
        public static List<T> Copy<T>(this IList<T> This)
        {
            return This.ToList();
        }
    }
}