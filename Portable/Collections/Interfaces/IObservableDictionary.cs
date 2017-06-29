using System.Collections;
using System.Collections.Generic;

namespace ClassLibrary.Portable.Collections.Interfaces
{
    public interface IObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IObservableCollection, IObservableCollection<KeyValuePair<TKey, TValue>>
    {
    }
}
