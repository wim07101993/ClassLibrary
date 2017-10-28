using System.Collections.Generic;
using ClassLibrary.Portable.Extensions;

namespace ClassLibrary.Portable.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// List is a wrapping class around the <see cref="System.Collections.Generic.List{T}" /> to add functionallity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class List<T> : System.Collections.Generic.List<T>
    {
        #region CONSTRUCTORS

        /// <inheritdoc />
        /// <summary>
        /// Constructs a List. The list is initially empty and has a capacity
        /// of zero. Upon adding the first element to the list the capacity is
        /// increased to 16, and then increased in multiples of two as required.
        /// </summary>
        public List()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructs a List with a given initial capacity. The list is
        /// initially empty, but will have room for the given number of elements
        /// before any reallocations are required.
        /// </summary>
        /// <param name="capacity"></param>
        public List(int capacity) : base(capacity)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructs a List, copying the contents of the given collection. The
        /// size and capacity of the new list will both be equal to the size of the
        /// given collection.
        /// </summary>
        /// <param name="collection"></param>
        public List(IEnumerable<T> collection) : base(collection)
        {
        }

        #endregion CONSTRUCTORS

        #region METHODS

        /// <summary>
        /// Adds the given element <see cref="t"/> to the <see cref="List{T}"/> collection.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<T> operator +(List<T> list, T t)
        {
            list.Add(t);
            return list;
        }

        /// <summary>
        /// Adds the given collection of elements <see cref="ts"/> to the <see cref="List{T}"/> collection.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static List<T> operator +(List<T> list, IEnumerable<T> ts)
        {
            list.AddRange(ts);
            return list;
        }

        /// <summary>
        /// Removes the given element <see cref="t"/> to the <see cref="List{T}"/> collection.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<T> operator -(List<T> list, T t)
        {
            list.Remove(t);
            return list;
        }

        /// <summary>
        /// Removes the given collection of elements <see cref="ts"/> to the <see cref="List{T}"/> collection.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static List<T> operator -(List<T> list, IEnumerable<T> ts)
        {
            list.RemoveRange(ts);
            return list;
        }

        #endregion METHODS
    }
}