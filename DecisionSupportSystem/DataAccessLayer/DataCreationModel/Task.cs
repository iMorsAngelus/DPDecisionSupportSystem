using System.Collections.Generic;

namespace DecisionSupportSystem.DataAccessLayer.DataCreationModel
{
    public class Task : BaseEntity
    {
        private string _name;
        private string _description;

        public string Name
        {
            get => _name;
            set
            {
                _name = value; 
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

        public virtual ICollection<Alternative> Alternatives { get; set; } = new List<Alternative>();
        public virtual ICollection<Criteria> Criterias { get; set; } = new List<Criteria>();
        public virtual ICollection<AlternativePriority> AlternativePriorityVector { get; set; } = new List<AlternativePriority>();
        public virtual ICollection<PairAlternative> PairAlternatives { get; set; } = new List<PairAlternative>();
        public virtual ICollection<CriteriaPriority> CriteriaPriorityVector { get; set; } = new List<CriteriaPriority>();
        public virtual ICollection<PairCriteria> PairCriterias { get; set; } = new List<PairCriteria>();
    }
}