using System.Collections.ObjectModel;
using System.ComponentModel;


namespace ClassLibrary.Prism.Interfaces
{
    public interface ICollectionVM<T> : INotifyPropertyChanged
    {
        T SelectedItem { get; set; }
        ObservableCollection<T> Items { get; }
    }
}