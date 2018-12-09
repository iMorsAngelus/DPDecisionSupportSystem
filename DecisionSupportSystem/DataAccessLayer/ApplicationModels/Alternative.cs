using DecisionSupportSystem.PresentationLayer;

namespace DecisionSupportSystem.DataAccessLayer.ApplicationModels
{
    public class Alternative : NotifyPropertyChanged
    {
        private string _alternativeName;
        private double _alternativeValue;
        private string _alternativeDescription;

        public string AlternativeName
        {
            get => _alternativeName;
            set
            {
                _alternativeName = value;
                OnPropertyChanged();
            }
        }

        public double AlternativeValue
        {
            get => _alternativeValue;
            set
            {
                _alternativeValue = value;
                OnPropertyChanged();
            }
        }

        public string AlternativeDescription
        {
            get => _alternativeDescription;
            set
            {
                _alternativeDescription = value;
                OnPropertyChanged();
            }
        }
    }
}