using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;


namespace ClassLibrary.Portable.Collections.Interfaces
{
    public interface IObservableCollection : INotifyCollectionChanged, INotifyPropertyChanged, ICollection
    {
    }

    public interface IObservableCollection<T> : INotifyCollectionChanged, INotifyPropertyChanged, IList<T>
    {
    }
}
