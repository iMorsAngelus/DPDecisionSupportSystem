using DecisionSupportSystem.DataAccessLayer.DataCreationModel;
using DecisionSupportSystem.DataAccessLayer.DbModels;
using DecisionSupportSystem.PresentationLayer.Common;
using System.Linq;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    class InputViewModel : ViewModelBase
    {
        private readonly IDataBaseProvider _provider;

        private int _CriteriasCount;
        private int _AlternativesCount;

        public InputViewModel(IDataBaseProvider provider)
        {
            _provider = provider;

            DisplayName = "InputViewModel";
        }

        public CollectionView<Criteria> Criterias { get; private set; }

        public CollectionView<Alternative> Alternatives { get; private set; }

        public int CriteriaCount
        {
            get => _CriteriasCount;
            set
            {
                if (_CriteriasCount == value) return;

                if (_CriteriasCount > value)
                {
                    var removableCriteria = Criterias.PageFilteredCollection.Last();
                    Criterias.SourceCollection.Remove(removableCriteria);
                }
                else
                {
                    var addCriteria = new Criteria
                    {
                        TaskId = _provider.CurrentTask.ID,
                        Name = "",
                        Description = ""
                    };
                    Criterias.SourceCollection.Add(addCriteria);
                }

                _CriteriasCount = value;
                OnPropertyChanged();
            }
        }

        public int AlternativeCount
        {
            get => _AlternativesCount;
            set
            {
                if (_AlternativesCount == value) return;

                if (_AlternativesCount > value)
                {
                    var removableAlternative = Alternatives.PageFilteredCollection.Last();
                    Alternatives.SourceCollection.Remove(removableAlternative);
                }
                else
                {
                    var addAlternative = new Alternative
                    {
                        TaskId = _provider.CurrentTask.ID,
                        Name = "",
                        Description = ""
                    };
                    Alternatives.SourceCollection.Add(addAlternative);
                }

                _AlternativesCount = value;
                OnPropertyChanged();
            }
        }

        public override void UpdateDataOnPage()
        {
            Criterias = new CollectionView<Criteria>(10, _provider.CurrentTask.Criterias, _provider);
            Alternatives = new CollectionView<Alternative>(10, _provider.CurrentTask.Alternatives, _provider);
            _CriteriasCount = _provider.CurrentTask.Criterias.Count;
            _AlternativesCount = _provider.CurrentTask.Alternatives.Count;
        }
    }
}