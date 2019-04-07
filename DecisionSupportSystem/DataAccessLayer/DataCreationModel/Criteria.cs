using System;
using System.Collections.Generic;

namespace DecisionSupportSystem.DataAccessLayer.DataCreationModel
{
    public class Criteria : BaseEntity
    {
        private string _name;
        private string _description;
        public Guid TaskId { get; set; }

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

        public virtual Task Task { get; set; }

        public virtual ICollection<CriteriaPriority> CriteriaPriorityVector { get; set; } = new List<CriteriaPriority>();
        public virtual ICollection<AlternativePriority> AlternativePriorityVector { get; set; } = new List<AlternativePriority>();
    }
}