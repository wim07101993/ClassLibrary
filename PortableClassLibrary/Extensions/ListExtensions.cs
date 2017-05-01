using System;
using System.Collections;
using System.Collections.Generic;


namespace PortableClassLibrary.Extensions
{
    public static class ListExtensions
    {
        public static int FindIndex<T>(this IList<T> This, Predicate<T> match)
        {
            for (var i = 0; i < This.Count; i++)
                if (match(This[i]))
                    return i;

            return -1;
        }

        public static void AddRange(this IList This, IEnumerable itemsToAdd)
        {
            foreach (var item in itemsToAdd)
                This.Add(item);
        }

        public static void RemoveRange(this IList This, IEnumerable itemsToRemove)
        {
            foreach (var item in itemsToRemove)
                This.Remove(item);
        }

        public static void RemoveRange(this IList This, IEnumerable<int> indexesToRemoveAt)
        {
            foreach (var i in indexesToRemoveAt)
                This.RemoveAt(i);
        }

        public static void RemoveLast(this IList This) => This.RemoveAt(This.Count - 1);
        public static void RemoveFirst(this IList This) => This.RemoveAt(0);
    }
}
