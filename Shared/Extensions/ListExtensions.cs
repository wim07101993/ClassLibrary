using System;
using System.Collections.Generic;

namespace Shared.Extensions
{
    public static class ListExtensions
    {
        #region CONVERSION

        #endregion CONVERSION


        #region ADDING

        public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
        {
            foreach (var t in collection)
                list.Add(t);
        }

        #endregion ADDING


        #region FINDING

        public static int IndexOfFirst<T>(this IList<T> list, Func<T, bool> predicate)
        {
            for (var i = 0; i < list.Count; i++)
                if (predicate(list[i]))
                    return i;
            return -1;
        }

        public static List<int> IndexOfWhere<T>(this IList<T> list, Func<T, bool> predicate)
        {
            var indexes = new List<int>();

            for (var i = 0; i < list.Count; i++)
                if (predicate(list[i]))
                    indexes.Add(i);

            return indexes.Count == 0
                ? null
                : indexes;
        }

        public static int IndexOfLast<T>(this IList<T> list, Func<T, bool> predicate)
        {
            for (var i = list.Count - 1; i >= 0; i--)
                if (predicate(list[i]))
                    return i;
            return -1;
        }

        #endregion FINDING


        #region REMOVING

        public static bool RemoveFirst<T>(this IList<T> list, Func<T, bool> predicate)
        {
            var index = list.IndexOfFirst(predicate);
            if (index == -1)
                return false;
            list.RemoveAt(index);
            return true;
        }

        public static bool RemoveWhere<T>(this IList<T> list, Func<T, bool> predicate)
        {
            var indexes = list.IndexOfWhere(predicate);

            if (indexes == null)
                return false;
            
            foreach (var i in indexes)
                list.RemoveAt(i);

            return true;
        }
        
        public static bool RemoveLast<T>(this IList<T> list, Func<T, bool> predicate)
        {
            var index = list.IndexOfLast(predicate);
            if (index == -1)
                return false;
            list.RemoveAt(index);
            return true;
        }

        #endregion REMOVING
    }
}