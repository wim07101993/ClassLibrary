using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shared.Extensions
{
    public static class EnumerableExtensions
    {
        #region CONVERSION

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
            => new ObservableCollection<T>(collection);

        #endregion CONVERSION


        public static IEnumerable<T> GetBetween<T>(this IEnumerable<T> collection, Func<T, bool> startPredicate,
            Func<T, bool> endPredicate, bool includeStart = false, bool includeEnd = false)
        {
            var started = false;
            using (var enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (!started && startPredicate(enumerator.Current))
                    {
                        started = true;
                        if (includeStart)
                            yield return enumerator.Current;
                    }else if (started && endPredicate(enumerator.Current))
                    {
                        if (includeEnd)
                            yield return enumerator.Current;
                        break;
                    }
                    else if (started)
                        yield return enumerator.Current;
                }
            }
        }
    }
}