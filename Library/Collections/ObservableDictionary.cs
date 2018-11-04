using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shared.Extensions;

namespace Shared.Collections
{
    public class ObservableDictionary<TKey, TValue> :
        System.Collections.ObjectModel.ObservableCollection<KeyValuePair<TKey, TValue>>,
        IDictionary<TKey, TValue>, IDictionary
    {
        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                return this.First(x => x.Key.Equals(key)).Value;
            }

            set
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                var index = this.IndexOfFirst(x => x.Key.Equals(key));

                if (index < 0)
                    throw new ArgumentException("key not found", nameof(key));

                this[index] = new KeyValuePair<TKey, TValue>(key, value);
            }
        }

        public ICollection<TKey> Keys => this.Select(x => x.Key).ToList();

        public ICollection<TValue> Values => this.Select(x => x.Value).ToList();


        public virtual void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public virtual bool ContainsKey(TKey key) => Keys.Contains(key);

        public virtual bool Remove(TKey key)
        {
            try
            {
                var index = this.IndexOfFirst(x => x.Key.Equals(key));

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

        public virtual bool TryGetValue(TKey key, out TValue value)
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


        object IDictionary.this[object key]
        {
            get => this[(TKey) key];
            set => this[(TKey) key] = (TValue) value;
        }

        ICollection IDictionary.Keys => Keys.ToList();

        ICollection IDictionary.Values => Values.ToList();

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;


        void IDictionary.Add(object key, object value)
        {
            if (!(key is TKey))
                throw new ArgumentException("wrong type", nameof(key));
            if (!(value is TValue))
                throw new ArgumentException("wrong type", nameof(value));

            Add((TKey) key, (TValue) value);
        }

        bool IDictionary.Contains(object key)
        {
            if (!(key is TKey))
                throw new ArgumentException("wrong type", nameof(key));

            return Keys.Contains((TKey) key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator() => throw new NotImplementedException();

        void IDictionary.Remove(object key)
        {
            if (!(key is TKey))
                throw new ArgumentException("wrong type", nameof(key));

            Remove((TKey) key);
        }
    }
}