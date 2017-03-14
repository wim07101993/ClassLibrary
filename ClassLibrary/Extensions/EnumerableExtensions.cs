using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace ClassLibrary.Extensions
{
    public static class EnumerableExtensions
    {
        public static string EnumerableToString(this IEnumerable This, Orientation orientation = Orientation.Horizontal)
        {
            var ret = "";
            if (orientation == Orientation.Horizontal)
            {
                ret = Enumerable
                    .Cast<object>(This)
                    .Aggregate(ret, (current, t) => current + (t + ", "));

                return string.IsNullOrEmpty(ret) ? "" : ret.Substring(0, ret.Length - 2);
            }

            ret = Enumerable
                .Cast<object>(This)
                .Aggregate(ret, (current, t) => current + (t + "\r\n"));

            return string.IsNullOrEmpty(ret) ? "" : ret.Substring(0, ret.Length - 4);
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> This)
        {
            var ret = new ObservableCollection<T>();

            ret.AddRange(This);

            return ret;
        }

        public static T Find<T>(this IEnumerable<T> This, Predicate<T> match)
        {
            foreach (var t in This)
                if (match(t))
                    return t;

            return default(T);
        }
        public static IList<T> FindAll<T>(this IEnumerable<T> This, Predicate<T> match)
        {
            var ret = new List<T>();

            foreach (var t in This)
                if (match(t))
                    ret.Add(t);

            return ret.Count != 0 ? ret : default(List<T>);
        }

        public static IList MergeEnumerables(this IEnumerable<IEnumerable> This)
        {
            var ret = new List<object>();

            foreach (var enumerable in This)
                ret.AddRange(enumerable);

            return ret;
        }

        public static int Count(this IEnumerable This)
        {
            return Enumerable.Count(Enumerable.Cast<object>(This));
        }

        public static bool IsNullOrEmpty(IEnumerable enumerable)
        {
            return enumerable == null || enumerable.Count() <= 0;
        }
    }
}
