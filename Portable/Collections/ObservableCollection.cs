using System.Collections.Generic;
using ClassLibrary.Portable.Collections.Interfaces;


namespace ClassLibrary.Portable.Collections
{
    public class ObservableCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>,
        IReadOnlyObservableCollection<T>
    {
        #region CONSTRUCTORS

        public ObservableCollection()
        {
        }

        public ObservableCollection(List<T> list)
            : base(list)
        {
        }

        public ObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        #endregion CONSTRUCTORS
    }
}