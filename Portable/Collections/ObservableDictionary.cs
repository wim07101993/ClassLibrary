using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Portable.Collections.Interfaces;
using ClassLibrary.Portable.Extensions;


namespace ClassLibrary.Portable.Collections
{
    /// <summary>
    /// Represents an Observable Collection of KeyValuePairs. 
    /// </summary>
    /// <typeparam name="TKey">Type of the Keys</typeparam>
    /// <typeparam name="TValue">Type of the Values</typeparam>
    public class ObservableDictionary<TKey, TValue> : ObservableCollection<KeyValuePair<TKey, TValue>>,
        IObservableDictionary<TKey, TValue>
    {
        #region IDICTIONARY<>

        #region PROPERTIES

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary{TKey,TValue}"/>.
        /// </para>
        /// 
        /// Sets or gets the value that is connected by the <see cref="KeyValuePair{TKey,TValue}"/> to the <see cref="key"/>.
        /// If the key doesn't exist, an <see cref="ArgumentException"/> is thrown.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value of the key</returns>
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

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary{TKey,TValue}"/>.
        /// </para>
        /// 
        /// Returns a <see cref="ICollection{TKey}"/> of all the keys in the <see cref="ObservableDictionary{TKey,TValue}"/>.
        /// </summary>
        public ICollection<TKey> Keys => this.Select(x => x.Key).ToList();

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary{TKey,TValue}"/>.
        /// </para>
        /// 
        /// Returns a <see cref="ICollection{TValue}"/> of all the values in the <see cref="ObservableDictionary{TKey,TValue}"/>.
        /// </summary>
        public ICollection<TValue> Values => this.Select(x => x.Value).ToList();

        #endregion PROPERTIES


        #region METHODS

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary{TKey,TValue}"/>.
        /// </para>
        /// 
        /// Adds a new <see cref="KeyValuePair{TKey,TValue}"/> to the <see cref="ObservableDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary{TKey,TValue}"/>.
        /// </para>
        /// 
        /// Returns if the <see cref="ObservableDictionary{TKey,TValue}"/> contains the key <see cref="key"/>
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>
        /// True: <see cref="ObservableDictionary{TKey,TValue}"/> contains the key. 
        /// False: <see cref="ObservableDictionary{TKey,TValue}"/> doesn't contain the key
        /// </returns>
        public virtual bool ContainsKey(TKey key) => Keys.Contains(key);

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary{TKey,TValue}"/>.
        /// </para>
        /// 
        /// Removes the First <see cref="KeyValuePair{TKey,TValue}"/> that has the <see cref="key"/> from the <see cref="ObservableDictionary{TKey,TValue}"/>.
        /// Returns true if the key was found.
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>
        /// True: key was found
        /// False: key was not found
        /// </returns>
        public virtual bool Remove(TKey key)
        {
            try
            {
                var index = this.FindIndex(x => x.Key.Equals(key));

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

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary{TKey,TValue}"/>.
        /// </para>
        /// 
        /// Tries to get the value behind the <see cref="KeyValuePair{TKey,TValue}"/> that holds the key.
        /// If an error occured, false is returned and the out value parameter is <code>default(TValue)</code>
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">out value</param>
        /// <returns>
        /// True: succesfull
        /// False: an error occured.
        /// </returns>
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

        #endregion METHODS

        #endregion IDICTIONARY<>


        #region IDICTIONARY

        #region PROPERTIES

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary"/>.
        /// </para>
        /// 
        /// Executes <code>this[(Tkey)key]</code>
        /// 
        /// This means an exception is thrown when on of the casts was invalid.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object IDictionary.this[object key]
        {
            get => this[(TKey) key];
            set => this[(TKey) key] = (TValue) value;
        }

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary"/>.
        /// </para>
        /// 
        /// Returns a <see cref="ICollection"/> of all the keys in the <see cref="ObservableDictionary{TKey,TValue}"/>.
        /// </summary>
        ICollection IDictionary.Keys => Keys.ToList();

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary"/>.
        /// </para>
        /// 
        /// Returns a <see cref="ICollection"/> of all the values in the <see cref="ObservableDictionary{TKey,TValue}"/>.
        /// </summary>
        ICollection IDictionary.Values => Values.ToList();

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary"/>.
        /// </para>
        /// 
        /// The <see cref="ObservableDictionary{TKey,TValue}"/> is not read only: Returns false;
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary"/>.
        /// </para>
        /// 
        /// The <see cref="ObservableDictionary{TKey,TValue}"/> has no fixed size: Returns false;
        /// </summary>
        public bool IsFixedSize => false;

        #endregion PROPERTIES


        #region METHODS

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary"/>.
        /// </para>
        /// 
        /// Executes <code>Add(TKey key, TValue value)</code> implemented from the <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        void IDictionary.Add(object key, object value)
        {
            if (!(key is TKey))
                throw new ArgumentException("wrong type", nameof(key));
            if (!(value is TValue))
                throw new ArgumentException("wrong type", nameof(value));

            Add((TKey) key, (TValue) value);
        }

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary"/>
        /// </para>
        /// 
        /// Executes <code>ConainsKey(TKey key)</code> implemented from the <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>
        /// True: <see cref="ObservableDictionary{TKey,TValue}"/> contains the key. 
        /// False: <see cref="ObservableDictionary{TKey,TValue}"/> doesn't contain the key
        /// </returns>
        bool IDictionary.Contains(object key)
        {
            if (!(key is TKey))
                throw new ArgumentException("wrong type", nameof(key));

            return Keys.Contains((TKey) key);
        }

        /// <summary>
        /// Is not implemented => <see cref="NotImplementedException"/> is thrown when executed!
        /// </summary>
        /// <returns></returns>
        IDictionaryEnumerator IDictionary.GetEnumerator() => throw new NotImplementedException();

        /// <summary>
        /// <para>
        /// Implemented from <see cref="IDictionary"/>.
        /// </para>
        /// 
        /// Executes <code>Remove(Tkey)</code> implemented from the<see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">key</param>
        void IDictionary.Remove(object key)
        {
            if (!(key is TKey))
                throw new ArgumentException("wrong type", nameof(key));

            Remove((TKey) key);
        }

        #endregion METHODS

        #endregion IDICTIONARY
    }
}