using DecisionSupportSystem.PresentationLayer;

namespace DecisionSupportSystem.DataAccessLayer.ApplicationModels
{
    public class MarkDescription : NotifyPropertyChanged
    {
        private string _valueDegree;
        private string _description;
        private string _comment;

        public string ValueDegree
        {
            get => _valueDegree;
            set
            {
                _valueDegree = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }
    }
}