using System.Collections.ObjectModel;


namespace ClassLibrary.Prism.Interfaces
{
    public interface ICollectionVM<T>
    {
        T SelectedItem { get; set; }
        ObservableCollection<T> Items { get; }
    }
}