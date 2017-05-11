using System.Collections.ObjectModel;


namespace Prism.Interfaces
{
    public interface ICollectionVM<T>
    {
        T SelectedItem { get; set; }
        ObservableCollection<T> Items { get; }
    }
}