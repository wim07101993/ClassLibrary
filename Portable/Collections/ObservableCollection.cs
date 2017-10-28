using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClassLibrary.Portable.Collections.Interfaces;
using ClassLibrary.Portable.Extensions;


namespace ClassLibrary.Portable.Collections
{
    /// <summary>
    /// Implementation of a dynamic data collection based on generic Collection&lt;T&gt;,
    /// implementing INotifyCollectionChanged to notify listeners
    /// when items get added, removed or the whole list is refreshed.
    /// </summary>
    public class ObservableCollection<T> : Collection<T>, IObservableCollection<T>, IObservableCollection
    {
        #region FIELDS

        // This must agree with Binding.IndexerName.  It is declared separately
        // here so as to avoid a dependency on PresentationFramework.dll.
        private const string IndexerName = "Item[]";

        private readonly SimpleMonitor _monitor = new SimpleMonitor();

        #endregion FIELDS

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of ObservableCollection that is empty and has default initial capacity.
        /// </summary>
        public ObservableCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ObservableCollection class
        /// that contains elements copied from the specified list
        /// </summary>
        /// <param name="list">The list whose elements are copied to the new list.</param>
        /// <remarks>
        /// The elements are copied onto the ObservableCollection in the
        /// same order they are read by the enumerator of the list.
        /// </remarks>
        /// <exception cref="ArgumentNullException"> list is a null reference </exception>
        public ObservableCollection(IReadOnlyCollection<T> list)
            : base(list != null ? new List<T>(list.Count) : null)
        {
            // Workaround for VSWhidbey bug 562681 (tracked by Windows bug 1369339).
            // We should be able to simply call the base(list) ctor.  But Collection<T>
            // doesn't copy the list (contrary to the documentation) - it uses the
            // list directly as its storage.  So we do the copying here.
            // 
            CopyFrom(list);
        }

        /// <summary>
        /// Initializes a new instance of the ObservableCollection class that contains
        /// elements copied from the specified collection and has sufficient capacity
        /// to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <remarks>
        /// The elements are copied onto the ObservableCollection in the
        /// same order they are read by the enumerator of the collection.
        /// </remarks>
        /// <exception cref="ArgumentNullException"> collection is a null reference </exception>
        public ObservableCollection(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            CopyFrom(collection);
        }

        #endregion CONSTRUCTORS


        #region METHODS

        /// <summary>
        /// Move item at oldIndex to newIndex.
        /// </summary>
        public void Move(int oldIndex, int newIndex)
        {
            MoveItem(oldIndex, newIndex);
        }
        
        #region Protected Methods

        /// <inheritdoc />
        /// <summary>
        /// Called by base class Collection&lt;T&gt; when the list is being cleared;
        /// raises a CollectionChanged event to any listeners.
        /// </summary>
        protected override void ClearItems()
        {
            CheckReentrancy();
            base.ClearItems();
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionReset();
        }

        /// <inheritdoc />
        /// <summary>
        /// Called by base class Collection&lt;T&gt; when an item is removed from list;
        /// raises a CollectionChanged event to any listeners.
        /// </summary>
        protected override void RemoveItem(int index)
        {
            CheckReentrancy();
            var removedItem = this[index];

            base.RemoveItem(index);

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, removedItem, index);
        }

        /// <inheritdoc />
        /// <summary>
        /// Called by base class Collection&lt;T&gt; when an item is added to list;
        /// raises a CollectionChanged event to any listeners.
        /// </summary>
        protected override void InsertItem(int index, T item)
        {
            CheckReentrancy();
            base.InsertItem(index, item);

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        /// <inheritdoc />
        /// <summary>
        /// Called by base class Collection&lt;T&gt; when an item is set in list;
        /// raises a CollectionChanged event to any listeners.
        /// </summary>
        protected override void SetItem(int index, T item)
        {
            CheckReentrancy();
            var originalItem = this[index];
            base.SetItem(index, item);

            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, originalItem, item, index);
        }

        /// <summary>
        /// Called by base class ObservableCollection&lt;T&gt; when an item is to be moved within the list;
        /// raises a CollectionChanged event to any listeners.
        /// </summary>
        protected virtual void MoveItem(int oldIndex, int newIndex)
        {
            CheckReentrancy();

            var removedItem = this[oldIndex];

            base.RemoveItem(oldIndex);
            base.InsertItem(newIndex, removedItem);

            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Move, removedItem, newIndex, oldIndex);
        }


        /// <summary>
        /// Raises a PropertyChanged event (per <see cref="INotifyPropertyChanged" />).
        /// </summary>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
            => PropertyChanged?.Invoke(this, e);

        /// <summary>
        /// PropertyChanged event (per <see cref="INotifyPropertyChanged" />).
        /// </summary>
#if !FEATURE_NETCORE
        [field: NonSerialized]
#endif
        protected virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise CollectionChanged event to any listeners.
        /// Properties/methods modifying this ObservableCollection will raise
        /// a collection changed event through this virtual method.
        /// </summary>
        /// <remarks>
        /// When overriding this method, either call its base implementation
        /// or call <see cref="BlockReentrancy"/> to guard against reentrant collection changes.
        /// </remarks>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged == null)
                return;

            using (BlockReentrancy())
            {
                CollectionChanged?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Disallow reentrant attempts to change this collection. E.g. a event handler
        /// of the CollectionChanged event is not allowed to make changes to this collection.
        /// </summary>
        /// <remarks>
        /// typical usage is to wrap e.g. a OnCollectionChanged call with a using() scope:
        /// <code>
        ///         using (BlockReentrancy())
        ///         {
        ///             CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, item, index));
        ///         }
        /// </code>
        /// </remarks>
        protected IDisposable BlockReentrancy()
        {
            _monitor.Enter();
            return _monitor;
        }

        /// <summary> Check and assert for reentrant attempts to change this collection. </summary>
        /// <exception cref="InvalidOperationException"> raised when changing the collection
        /// while another collection change is still being notified to other listeners </exception>
        protected void CheckReentrancy()
        {
            if (!_monitor.Busy)
                return;

            // we can allow changes if there's only one listener - the problem
            // only arises if reentrant changes make the original event args
            // invalid for later listeners.  This keeps existing code working
            // (e.g. Selector.SelectedItems).
            if (CollectionChanged != null && CollectionChanged.GetInvocationList().Length > 1)
                throw new InvalidOperationException("Reentrancy is not allowed.");
        }

        #endregion Protected Methods

        #region Private Methods

        private void CopyFrom(IEnumerable<T> collection)
        {
            var items = Items;
            if (collection == null)
                return;

            using (var enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    items.Add(enumerator.Current);
                }
            }
        }

        /// <summary>
        /// Helper to raise a PropertyChanged event  />).
        /// </summary>
        private void OnPropertyChanged(string propertyName)
            => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Helper to raise CollectionChanged event to any listeners
        /// </summary>
        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));

        /// <summary>
        /// Helper to raise CollectionChanged event to any listeners
        /// </summary>
        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));

        /// <summary>
        /// Helper to raise CollectionChanged event to any listeners
        /// </summary>
        private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem,
            int index)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));

        /// <summary>
        /// Helper to raise CollectionChanged event with action == Reset to any listeners
        /// </summary>
        private void OnCollectionReset()
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        #endregion Private Methods

        #endregion METHODS

        /// <summary>
        /// Adds the given element <see cref="t"/> to the <see cref="ObservableCollection{T}"/> collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ObservableCollection<T> operator +(ObservableCollection<T> collection, T t)
        {
            collection.Add(t);
            return collection;
        }

        /// <summary>
        /// Adds the given collection of elements <see cref="ts"/> to the <see cref="ObservableCollection{T}"/> collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static ObservableCollection<T> operator +(ObservableCollection<T> collection, IEnumerable<T> ts)
        {
            collection.ToCPObservableCollection();
            collection.AddRange(ts);
            return collection;
        }

        /// <summary>
        /// Removes the given element <see cref="t"/> to the <see cref="ObservableCollection{T}"/> collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ObservableCollection<T> operator -(ObservableCollection<T> collection, T t)
        {
            collection.ToCPObservableCollection();
            collection.Remove(t);
            return collection;
        }

        /// <summary>
        /// Removes the given collection of elements <see cref="ts"/> to the <see cref="ObservableCollection{T}"/> collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static ObservableCollection<T> operator -(ObservableCollection<T> collection, IEnumerable<T> ts)
        {
            collection.ToCPObservableCollection();
            collection.RemoveRange(ts);
            return collection;
        }


        #region EVENTS

        /// <summary>
        /// PropertyChanged event (per <see cref="INotifyPropertyChanged" />).
        /// </summary>
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => PropertyChanged += value;
            remove => PropertyChanged -= value;
        }

        /// <summary>
        /// Occurs when the collection changes, either by adding or removing an item.
        /// </summary>
        /// <remarks>
        /// see <seealso cref="INotifyCollectionChanged"/>
        /// </remarks>
#if !FEATURE_NETCORE
        [field: NonSerialized]
#endif
        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion EVENTS


        #region TYPES

        // this class helps prevent reentrant calls
#if !FEATURE_NETCORE
        [Serializable]
        [TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
#endif
        private class SimpleMonitor : IDisposable
        {
            public void Enter()
            {
                ++_busyCount;
            }

            public void Dispose()
            {
                --_busyCount;
            }

            public bool Busy => _busyCount > 0;

            private int _busyCount;
        }

        #endregion TYPES
    }
}