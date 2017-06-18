using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using ClassLibrary.Portable.Extensions;
using Prism.Mvvm;


// ReSharper disable ExplicitCallerInfoArgument


namespace ClassLibrary.Prism
{
    public abstract class AObservableBase : BindableBase
    {
        #region FIELDS

        /// <summary>
        /// Dictionary to store all old values of properties
        /// </summary>
        private Dictionary<string, IList> _oldValueDictionary;

        #endregion FIELDS


        #region PROPERTIES

        /// <summary>
        /// Read-only-collection of all the properties that have been changed;
        /// </summary>
        public IReadOnlyCollection<string> ChangedProperties
            => new ReadOnlyCollection<string>(_oldValueDictionary.Keys.ToList());

        #endregion PROPERTIES


        #region METHODS

        /// <summary>
        /// Sets the <see cref="storage"/> to the value <see cref="value"/> if the value and storage are not equal and raises propertyChanged
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            // if the value equals the storage value or there is no propertyName, there is nothing to change
            if (Equals(storage, value) || string.IsNullOrWhiteSpace(propertyName))
                return false;

            // add old value to storage
            AddValueToOldValueDictionary(propertyName, value);

            // set the storage to the new value
            storage = value;
            // raise a property changed
            RaisePropertyChanged(propertyName);
            APropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Sets an <see cref="ObservableCollection{T}"/> to the value <see cref="value"/> if the value and storage are not equal.
        /// Subscribes to collectionChanged to add undo ability.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected virtual bool SetCollection<T>(ref ObservableCollection<T> storage, ObservableCollection<T> value,
            [CallerMemberName] string propertyName = null)
        {
            // if the value equals the storage value or there is no propertyName, there is nothing to change
            if (Equals(storage, value) || string.IsNullOrWhiteSpace(propertyName))
                return false;

            // add old value to storage
            AddValueToOldValueDictionary(propertyName, storage);

            // if storage is not null, unsubscribe from CollectionChanged
            if (storage != null)
                storage.CollectionChanged -= StorageOnCollectionChanged;

            // set the storage to the new value
            storage = value;

            // if storage not is null, subscribe to CollectionChanged
            if (storage != null)
                storage.CollectionChanged += StorageOnCollectionChanged;

            // raise a property changed
            RaisePropertyChanged(propertyName);
            APropertyChanged(propertyName);

            return true;

            // method to add collection to old value dictionary
            void StorageOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
                => AddValueToOldValueDictionary(propertyName, value);
        }

        /// <summary>
        /// Adds a value to the Old Value Dictionary. If the dictionary doesn't exist yet, it is created.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        private void AddValueToOldValueDictionary(string propertyName, object value)
        {
            // if dictionary doesn't exits, create it with the entry
            if (_oldValueDictionary == null)
                _oldValueDictionary = new Dictionary<string, IList>
                {
                    {
                        propertyName,
                        new List<object> {value}
                    }
                };
            // if dictionary already has values for propertyName add value to values list
            else if (_oldValueDictionary.ContainsKey(propertyName))
                _oldValueDictionary[propertyName].Add(value);
            // else just add new entry
            else
                _oldValueDictionary.Add(propertyName, new List<object> {value});
        }

        /// <summary>
        /// Resets property to the last time the property was set with the <see cref="SetProperty{T}"/> 
        /// method or <see cref="SetCollection{T}"/> method.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public bool Undo(string propertyName)
        {
            // if there is no dictionary, there is nothing to undo
            if (_oldValueDictionary == null)
                return false;

            // check if a propertyName has been entered
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Property cannot be null or whitespace", propertyName);

            // if the propertyName is not registred, ther is nothing to undo
            if (!_oldValueDictionary.ContainsKey(propertyName))
                return false;

            // get the property
            var property = GetType().GetProperty(propertyName);

            // check if property exists in this object
            if (property == null)
                throw new ArgumentException("Unknown property", propertyName);

            // set the value of the property to the last entered old value
            property.SetValue(this, _oldValueDictionary[propertyName].Last());
            // remove the last old value since it is now the current value
            _oldValueDictionary[propertyName].RemoveLast();

            // if the count of entered values for this property is null, it can be removed from the dictionary
            if (_oldValueDictionary[propertyName].Count == 0)
                _oldValueDictionary.Remove(propertyName);

            // raise a property change
            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Method that gets called when a property changed.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void APropertyChanged(string propertyName)
        {
        }

        #endregion METHODS
    }
}