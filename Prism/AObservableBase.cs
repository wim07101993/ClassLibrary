using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ClassLibrary.Portable.Extensions;
using Prism.Mvvm;


namespace ClassLibrary.Prism
{
    public abstract class AObservableBase : BindableBase
    {
        #region FIELDS

        private Dictionary<string, IList> _oldValueDictionary;

        #endregion FIELDS


        #region METHODS

        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            // if the value equals the storage value or there is no propertyName, there is nothing to change
            if (Equals(storage, value) || string.IsNullOrWhiteSpace(propertyName))
                return false;

            // check if old value dictionary exists and add the new value to it
            if (_oldValueDictionary == null)
                _oldValueDictionary = new Dictionary<string, IList>
                {
                    {
                        propertyName,
                        new List<object> {storage}
                    }
                };
            else if (_oldValueDictionary.ContainsKey(propertyName))
                _oldValueDictionary[propertyName].Add(storage);
            else
                _oldValueDictionary.Add(propertyName, new List<object> {storage});

            // set the storage to the new value
            storage = value;
            // raise a property changed
            RaisePropertyChanged(propertyName);
            APropertyChanged(propertyName);

            return true;
        }

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

        public virtual void APropertyChanged(string propertyName)
        {
        }

        #endregion METHODS
    }
}