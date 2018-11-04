using System.Collections.Generic;
using System.Linq;
using Shared.Extensions;

namespace Shared.Collections
{
    // Hopefully the operator wrappers can be added to extensions instead with C#8
    public sealed class Collection<T> : System.Collections.ObjectModel.Collection<T>
    {
        public Collection()
        {
        }

        public Collection(IList<T> collection) : base(collection)
        {
        }


        public static Collection<T> operator +(Collection<T> collection, T t)
        {
            collection.Add(t);
            return collection;
        }

        public static Collection<T> operator +(Collection<T> collection, IEnumerable<T> ts)
        {
            collection.AddRange(ts);
            return collection;
        }

        public static Collection<T> operator -(Collection<T> collection, T t)
        {
            collection.Remove(t);
            return collection;
        }

        public static Collection<T> operator -(Collection<T> collection, IEnumerable<T> ts)
        {
            collection.RemoveWhere(x => ts.Any(y => Equals(x, y)));
            return collection;
        }
    }
}