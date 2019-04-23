using DecisionSupportSystem.DataAccessLayer.DataCreationModel;
using DecisionSupportSystem.DataAccessLayer.DbModels;
using DecisionSupportSystem.PresentationLayer.Common;
using System.Linq;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    class InputViewModel : ViewModelBase
    {
        private readonly IDataBaseProvider _provider;

        private int _criteriasCount;
        private int _alternativesCount;

        public InputViewModel(IDataBaseProvider provider)
        {
            _provider = provider;

            DisplayName = "InputViewModel";
        }

        public CollectionView<Criteria> Criterias { get; private set; }

        public CollectionView<Alternative> Alternatives { get; private set; }

        public int CriteriaCount
        {
            get => _criteriasCount;
            set
            {
                if (_criteriasCount == value) return;

                if (_criteriasCount > value)
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
                _criteriasCount = value;
                Mediator.Notify("CanExecuteChanged");
                OnPropertyChanged();
            }
        }

        public int AlternativeCount
        {
            get => _alternativesCount;
            set
            {
                if (_alternativesCount == value) return;

                if (_alternativesCount > value)
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

                _alternativesCount = value;
                Mediator.Notify("CanExecuteChanged");
                OnPropertyChanged();
            }
        }

        public override void UpdateDataOnPage()
        {
            Criterias = new CollectionView<Criteria>(10, _provider.CurrentTask.Criterias, _provider);
            Alternatives = new CollectionView<Alternative>(10, _provider.CurrentTask.Alternatives, _provider);
            _criteriasCount = _provider.CurrentTask.Criterias.Count;
            _alternativesCount = _provider.CurrentTask.Alternatives.Count;
        }
    }
}