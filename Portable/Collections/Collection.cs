using System.Collections.Generic;
using ClassLibrary.Portable.Extensions;

namespace ClassLibrary.Portable.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// Collection is a wrapping class around the <see cref="System.Collections.ObjectModel.Collection{T}"/> to add functionallity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Collection<T> : System.Collections.ObjectModel.Collection<T>
    {
        #region CONSTRUCTORS

        /// <inheritdoc />
        /// <summary>
        /// Constructs a List. The list is initially empty and has a capacity
        /// of zero. Upon adding the first element to the list the capacity is
        /// increased to 16, and then increased in multiples of two as required.
        /// </summary>
        public Collection()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructs a List, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        /// <param name="collection"></param>
        public Collection(IList<T> collection) : base(collection)
        {
        }

        #endregion CONSTRUCTORS

        #region METHODS

        /// <summary>
        /// Adds the given element <see cref="t"/> to the <see cref="Collection{T}"/> collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Collection<T> operator +(Collection<T> collection, T t)
        {
            collection.Add(t);
            return collection;
        }

        /// <summary>
        /// Adds the given collection of elements <see cref="ts"/> to the <see cref="Collection{T}"/> collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static Collection<T> operator +(Collection<T> collection, IEnumerable<T> ts)
        {
            collection.AddRange(ts);
            return collection;
        }

        /// <summary>
        /// Removes the given element <see cref="t"/> to the <see cref="Collection{T}"/> collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Collection<T> operator -(Collection<T> collection, T t)
        {
            collection.Remove(t);
            return collection;
        }

        /// <summary>
        /// Removes the given collection of elements <see cref="ts"/> to the <see cref="Collection{T}"/> collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static Collection<T> operator -(Collection<T> collection, IEnumerable<T> ts)
        {
            collection.RemoveRange(ts);
            return collection;
        }

        #endregion METHODS
    }
}
