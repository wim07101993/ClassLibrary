using System.Collections.ObjectModel;
using ClassLibrary.Prism.Interfaces;
using Prism.Mvvm;


namespace ClassLibrary.Prism
{
    public class ACollectionVM<T> : BindableBase, ICollectionVM<T>
    {
        #region FIELDS

        private ObservableCollection<T> _items;
        private T _selectedItem;

        #endregion FIELDS


        #region PROPERTIES

        public ObservableCollection<T> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public T SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        #endregion PROPERTIES
    }
}