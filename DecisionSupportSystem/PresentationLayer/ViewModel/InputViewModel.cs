using System.Collections.ObjectModel;
using DecisionSupportSystem.DataAccessLayer;
using DecisionSupportSystem.DataAccessLayer.ApplicationModels;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    class InputViewModel : ViewModelBase, IPageViewModel
    {
        
        private int _criteriaCount;
        private int _alternativeCount;
        private int _selectedIndex;

        public InputViewModel(ObservableCollection<Criteria> criterias, ObservableCollection<Alternative> alternatives)
        {
            DisplayName = "InputViewModel";
            Criterias = criterias?? new ObservableCollection<Criteria>();
            Alternatives = alternatives?? new ObservableCollection<Alternative>();
        }

        public ObservableCollection<Criteria> Criterias { get; set; }

        public ObservableCollection<Alternative> Alternatives { get; set; }
        
        public int CriteriaCount
        {
            get => _criteriaCount;
            set
            {
                if (_criteriaCount == value) return;

                if (_criteriaCount > value)
                    Criterias.RemoveAt(Criterias.Count - 1);
                else Criterias.Add(new Criteria());

                _criteriaCount = value;
                OnPropertyChanged();
            }

        }

        public int AlternativeCount
        {
            get => _alternativeCount;
            set
            {
                if (_alternativeCount == value) return;

                if (_alternativeCount > value)
                    Alternatives.RemoveAt(Criterias.Count - 1);
                else Alternatives.Add(new Alternative());

                _alternativeCount = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }
    }
}