using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace ClassLibrary.Portable.Extensions
{
    /// <summary>
    /// A static class with extensions for the <see cref="IEnumerable"/> class.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Enum to tell if something is horizontal or vertical.
        /// </summary>
        public enum Orientation
        {
            Horizontal,
            Vertical
        }

        /// <summary>
        /// Creates a string that represents an enumerable by adding the different items to the string, seperated by comma or a new line (depending on the orientation). 
        /// </summary>
        /// <param name="This"></param>
        /// <param name="orientation"></param>
        /// <returns></returns>
        public static string EnumerableToString(this IEnumerable This, Orientation orientation = Orientation.Horizontal)
        {
            var ret = "";

            // if orientation is horizontal: create oneline string seperated by comma
            if (orientation == Orientation.Horizontal)
            {
                ret = This
                    .Cast<object>()
                    .Aggregate(ret, (current, t) => current + (t + ", "));

                return string.IsNullOrEmpty(ret) ? "" : ret.Substring(0, ret.Length - 2);
            }


            // if orientation is vertical: create multiline string seperated by newLine (\r\n)
            ret = This
                .Cast<object>()
                .Aggregate(ret, (current, t) => current + (t + "\r\n"));

            return string.IsNullOrEmpty(ret) ? "" : ret.Substring(0, ret.Length - 4);
        }

        /// <summary>
        /// Creates a new <see cref="ObservableCollection{T}"/> and adds al the value of this list to it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> This)
        {
            var collection = This as ObservableCollection<T>;
            return collection ?? new ObservableCollection<T>(This);
        }

        /// <summary>
        /// Creates a new <see cref="ClassLibrary.Portable.Collections.ObservableCollection{T}"/> and adds al the value of this list to it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <returns></returns>
        public static Collections.ObservableCollection<T> ToMyObservableCollection<T>(this IEnumerable<T> This)
        {
            var collection = This as Collections.ObservableCollection<T>;
            return collection ?? new Collections.ObservableCollection<T>(This);
        }


        /// <summary>
        /// Loops trough this enumerable to find a value that matches the <see cref="Predicate{T}"/> match.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static T Find<T>(this IEnumerable<T> This, Predicate<T> match)
        {
            foreach (var t in This)
                if (match(t))
                    return t;

            return default(T);
        }
        /// <summary>
        /// Loops trough all items of this enumerable to find all values that match the <see cref="Predicate{T}"/> match.
        /// (Just a Where claus by Linq).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static IList<T> FindAll<T>(this IEnumerable<T> This, Predicate<T> match)
            => This.Where(t => match(t)).ToList();
        
        /// <summary>
        /// Merges an <see cref="IEnumerable{T}"/> of <see cref="IEnumerable"/>. Adds all values to one <see cref="IList"/>.
        /// </summary>
        /// <param name="This"></param>
        /// <returns></returns>
        public static IList MergeEnumerables(this IEnumerable<IEnumerable> This)
        {
            var ret = new List<object>();

            foreach (var enumerable in This)
                ret.AddRange(enumerable);

            return ret;
        }

        /// <summary>
        /// Returns the number of items in this <see cref="IEnumerable"/>.
        /// </summary>
        /// <param name="This"></param>
        /// <returns></returns>
        public static int Count(this IEnumerable This) 
            => Enumerable.Count(This.Cast<object>());

        /// <summary>
        /// Checks if an <see cref="IEnumerable"/> is null or has no elements.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(IEnumerable enumerable) 
            => enumerable == null || enumerable.Count() <= 0;

        /// <summary>
        /// Searches for all elements that occure in one <see cref="IEnumerable{T}"/> but not in the other. (uses the FindDifferences method defined below).
        /// 
        /// if usePropertyComare is true: The extensionmethod PropertyCompare (defined in <see cref="ObjectExtensions"/>) is used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="firstCollection">first collection</param>
        /// <param name="secondCollection">second collection</param>
        /// <param name="itemsInFirstCollectionButNotInSecond">items that occure in the first collection but not in the second</param>
        /// <param name="itemsInSecondCollectionButNotInFirst">items that occure in the second collection but not in the first</param>
        /// <param name="usePropertyComparer"></param>
        public static void FindDifferences<T>(
            IEnumerable<T> firstCollection,
            IEnumerable<T> secondCollection,
            out IEnumerable<T> itemsInFirstCollectionButNotInSecond,
            out IEnumerable<T> itemsInSecondCollectionButNotInFirst,
            bool usePropertyComparer = true)
        {
            itemsInFirstCollectionButNotInSecond =
                FindDifferences(firstCollection, secondCollection, usePropertyComparer);
            itemsInSecondCollectionButNotInFirst =
                FindDifferences(secondCollection, firstCollection, usePropertyComparer);
        }

        /// <summary>
        /// Searches for the elements that occure in the <see cref="IEnumerable{T}"/> firstCollection but not in the <see cref="IEnumerable{T}"/> secondCollection.
        /// 
        /// if usePropertyComare is true: The extensionmethod PropertyCompare (defined in <see cref="ObjectExtensions"/>) is used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="firstCollection"></param>
        /// <param name="secondCollection">Collecion to compare with</param>
        /// <param name="usePropertyCompare"></param>
        /// <returns>All items that occure in first collectino but not in second.</returns>
        public static IEnumerable<T> FindDifferences<T>(
            this IEnumerable<T> firstCollection,
            IEnumerable<T> secondCollection,
            bool usePropertyCompare = true)
        {
            if (usePropertyCompare)
                return firstCollection.Where(
                    item => secondCollection.Find(x => x.PropertyCompare(item)) == null);

            return firstCollection.Where(
                item => secondCollection.Find(x => Equals(x, item)) == null);
        }
    }
}