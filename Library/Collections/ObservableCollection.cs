using System.Collections.Generic;
using System.Linq;
using Library.Extensions;

namespace Library.Collections
{
    public sealed class ObservableCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>
    {
        public ObservableCollection()
        {
        }

        public ObservableCollection(IList<T> collection) : base(collection)
        {
        }


        public static ObservableCollection<T> operator +(ObservableCollection<T> collection, T t)
        {
            collection.Add(t);
            return collection;
        }

        public static ObservableCollection<T> operator +(ObservableCollection<T> collection, IEnumerable<T> ts)
        {
            collection.AddRange(ts);
            return collection;
        }

        public static ObservableCollection<T> operator -(ObservableCollection<T> collection, T t)
        {
            collection.Remove(t);
            return collection;
        }

        public static ObservableCollection<T> operator -(ObservableCollection<T> collection, IEnumerable<T> ts)
        {
            collection.RemoveWhere(x => ts.Any(y => Equals(x, y)));
            return collection;
        }
    }
}