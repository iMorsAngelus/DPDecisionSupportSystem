using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using DecisionSupportSystem.Common;
using DecisionSupportSystem.DataAccessLayer.DataCreationModel;
using DecisionSupportSystem.DataAccessLayer.DbModels;

namespace DecisionSupportSystem.PresentationLayer.Common
{
    /// <summary>
    /// The collection for paging and filtering
    /// </summary>
    class CollectionView<T> : NotifyPropertyChanged
        where T:NotifyPropertyChanged
    {
        private readonly int _itemsPerPage;
        private readonly IDataBaseProvider _provider;
        private ExtendedObservableCollection<T> _pageFilteredCollection;
        private int _pageIndex = 1;
        private int _pagesCount = 1;
        private bool _canMoveFirst;
        private bool _canMoveLast;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionView{T}"/>
        /// </summary>
        /// <param name="itemsPerPage">Count of items per page.</param>
        /// <param name="sourceCollection">Collection of settings</param>
        /// <param name="provider">Db provider</param>
        public CollectionView(int itemsPerPage, ICollection<T> sourceCollection, IDataBaseProvider provider)
        {
            _itemsPerPage = itemsPerPage;
            _provider = provider;
            CanMoveLast = PageIndex != PagesCount;
            SourceCollection = new ObservableCollection<T>(sourceCollection);

            SourceCollection.CollectionChanged += OnCollectionChanged;
            Filter();
        }

        /// <summary>
        /// List of settings that were filtered per page
        /// </summary>
        public ExtendedObservableCollection<T> PageFilteredCollection
        {
            get => _pageFilteredCollection;
            set
            {
                _pageFilteredCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<T> SourceCollection { get; set; }

        /// <summary>
        /// Index of current page
        /// </summary>
        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                _pageIndex = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Count of all pages
        /// </summary>
        public int PagesCount
        {
            get => _pagesCount;
            set
            {
                _pagesCount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Return false if current page is first 
        /// </summary>
        public bool CanMoveFirst
        {
            get => _canMoveFirst;
            set
            {
                _canMoveFirst = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Return false if current page is last
        /// </summary>
        public bool CanMoveLast
        {
            get => _canMoveLast;
            set
            {
                _canMoveLast = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Filters the collection
        /// </summary>
        /// <returns></returns>
        public void Filter()
        {
            UpdatePagesCount(SourceCollection.Count);

            if (PageFilteredCollection != null)
            {
                PageFilteredCollection.CollectionChanged -= OnItemEdit;
            }

            PageFilteredCollection = FilterByCount(SourceCollection);
            PageFilteredCollection.CollectionChanged += OnItemEdit;

            CanMoveLast = PageIndex != PagesCount;
        }

        /// <summary>
        /// Display the first page
        /// </summary>
        public void MoveFirst()
        {
            PageIndex = 1;
            CanMoveLast = true;
            CanMoveFirst = false;
            Filter();
        }

        /// <summary>
        /// Display the previous page
        /// </summary>
        public void MovePrev()
        {
            PageIndex--;
            if (PageIndex == 1)
            {
                CanMoveFirst = false;
            }
            CanMoveLast = PagesCount != 1;
            Filter();
        }

        /// <summary>
        /// Display the next page
        /// </summary>
        public void MoveNext()
        {
            PageIndex++;
            if (PageIndex == PagesCount)
            {
                CanMoveLast = false;
            }
            CanMoveFirst = true;
            Filter();
        }

        /// <summary>
        /// Display the last page
        /// </summary>
        public void MoveLast()
        {
            PageIndex = PagesCount;
            CanMoveLast = false;
            CanMoveFirst = true;
            Filter();
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        switch (item)
                        {
                            case Criteria _:
                                _provider.CurrentTask.Criterias.Add(item as Criteria);
                                break;
                            case Alternative _:
                                _provider.CurrentTask.Alternatives.Add(item as Alternative);
                                break;
                        }

                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        switch (item)
                        {
                            case Criteria _:
                                _provider.ObservableCriterias.Remove(item as Criteria);
                                break;
                            case Alternative _:
                                _provider.ObservableAlternatives.Remove(item as Alternative);
                                break;
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _provider.SaveChanges();
            Filter();
        }

        private ExtendedObservableCollection<T> FilterByCount(IList<T> collection)
        {
            var filteredCollection = new ExtendedObservableCollection<T>();
            foreach (var item in collection)
            {
                var index = collection.IndexOf(item);
                if (index >= _itemsPerPage * (PageIndex - 1) && index < _itemsPerPage * PageIndex)
                    filteredCollection.Add(item);
            }
            return filteredCollection;
        }

        private void UpdatePagesCount(int itemsCount)
        {
            PagesCount = itemsCount % _itemsPerPage == 0
                ? itemsCount / _itemsPerPage
                : itemsCount / _itemsPerPage + 1;
        }

        private void OnItemEdit(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Replace)
                foreach (var oldItem in e.OldItems)
                {
                    int index;
                    switch (oldItem)
                    {
                        case Criteria _:
                            _provider.CurrentTask.Criterias.Remove(oldItem as Criteria);
                            _provider.CurrentTask.Criterias.Add(e.NewItems[e.OldItems.IndexOf(oldItem)] as Criteria);
                            break;
                        case Alternative _:
                            _provider.CurrentTask.Alternatives.Remove(oldItem as Alternative);
                            _provider.CurrentTask.Alternatives.Add(e.NewItems[e.OldItems.IndexOf(oldItem)] as Alternative);
                            break;
                    }
                }
            _provider.SaveChanges();
            Filter();
        }
    }
}
