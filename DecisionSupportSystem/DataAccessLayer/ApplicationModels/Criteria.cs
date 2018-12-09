using DecisionSupportSystem.PresentationLayer;

namespace DecisionSupportSystem.DataAccessLayer.ApplicationModels
{
    public class Criteria : NotifyPropertyChanged
    {
        private string _criteriaName;
        private string _criteriaDescription;

        public string CriteriaName
        {
            get => _criteriaName;
            set
            {
                _criteriaName = value;
                OnPropertyChanged();
            }
        }

        public string CriteriaDescription
        {
            get => _criteriaDescription;
            set
            {
                _criteriaDescription = value;
                OnPropertyChanged();
            }
        }
    }
}