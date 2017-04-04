using ClassLibrary.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClassLibrary.Collections
{
    public class ObservableDictionary<TKey, TValue> : ObservableCollection<KeyValuePair<TKey, TValue>>,
        IDictionary<TKey, TValue>,
        IDictionary
    {
        #region IMPLEMENTED



        #region IDICTIONARY<>

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                return this.Find(x => x.Key.Equals(key)).Value;
            }

            set
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                var index = this.FindIndex(x => x.Key.Equals(key));

                if (index < 0)
                    throw new ArgumentException("key not found", nameof(key));

                this[index] = new KeyValuePair<TKey, TValue>(key, value);
            }
        }

        public ICollection<TKey> Keys
        {
            get { return this.Select(x => x.Key).ToList(); }
        }
        public ICollection<TValue> Values
        {
            get { return this.Select(x => x.Value).ToList(); }
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool ContainsKey(TKey key)
        {
            return Keys.Contains(key);
        }

        public bool Remove(TKey key)
        {
            try
            {
                int index = this.FindIndex(x => x.Key.Equals(key));

                if (index < 0)
                    return false;

                RemoveItem(index);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            try
            {
                value = this[key];
                return true;
            }
            catch (Exception)
            {
                value = default(TValue);
                return false;
            }
        }

        #endregion IDICTIONARY<>



        #region IDICTIONARY

        object IDictionary.this[object key]
        {
            get { return this[(TKey)key]; }
            set { this[(TKey)key] = (TValue)value; }
        }

        ICollection IDictionary.Keys => Keys.ToList();
        ICollection IDictionary.Values => Values.ToList();

        public bool IsReadOnly => false;
        public bool IsFixedSize => false;

        bool IDictionary.Contains(object key)
        {
            if (!(key is TKey))
                throw (new ArgumentException("wrong type", nameof(key)));

            return Keys.Contains((TKey)key);
        }

        void IDictionary.Add(object key, object value)
        {
            if (!(key is TKey))
                throw (new ArgumentException("wrong type", nameof(key)));
            if (!(value is TValue))
                throw (new ArgumentException("wrong type", nameof(value)));

            Add((TKey)key, (TValue)value);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        void IDictionary.Remove(object key)
        {
            if (!(key is TKey))
                throw (new ArgumentException("wrong type", nameof(key)));

            Remove((TKey)key);
        }

        #endregion IDICTIONARY



        #endregion IMPLEMENTED
    }
}