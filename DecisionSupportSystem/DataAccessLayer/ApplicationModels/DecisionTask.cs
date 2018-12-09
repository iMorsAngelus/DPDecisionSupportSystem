using System.Collections.ObjectModel;
using DecisionSupportSystem.PresentationLayer;

namespace DecisionSupportSystem.DataAccessLayer.ApplicationModels
{
    public class DecisionTask : NotifyPropertyChanged
    {
        public string DecisionTaskName { get; set; }
        public ObservableCollection<Criteria> Criterias { get; set; }
        public ObservableCollection<Alternative> Alternatives { get; set; }
    }
}