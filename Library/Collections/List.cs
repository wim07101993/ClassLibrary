using System.Collections.Generic;
using System.Linq;
using Library.Extensions;

namespace Library.Collections
{
    public sealed class List<T> : System.Collections.Generic.List<T>
    {
        public List()
        {
        }

        public List(int capacity) : base(capacity)
        {
        }

        public List(IEnumerable<T> collection) : base(collection)
        {
        }

        public static List<T> operator +(List<T> list, T t)
        {
            list.Add(t);
            return list;
        }

        public static List<T> operator +(List<T> list, IEnumerable<T> ts)
        {
            list.AddRange(ts);
            return list;
        }

        public static List<T> operator -(List<T> list, T t)
        {
            list.Remove(t);
            return list;
        }

        public static List<T> operator -(List<T> list, IEnumerable<T> ts)
        {
            list.RemoveWhere(x => ts.Any(y => Equals(x, y)));
            return list;
        }
    }
}