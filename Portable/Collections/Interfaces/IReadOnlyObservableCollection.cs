using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;


namespace ClassLibrary.Portable.Collections.Interfaces
{
    public interface IReadOnlyObservableCollection<out T>: INotifyCollectionChanged, INotifyPropertyChanged, IReadOnlyList<T>
    {
    }
}
