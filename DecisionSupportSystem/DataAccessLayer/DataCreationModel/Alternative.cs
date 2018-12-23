using System;

namespace DecisionSupportSystem.DataAccessLayer.DataCreationModel
{
    public class Alternative : BaseEntity
    {
        private string _name;
        private string _description;
        private string _value;

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

        public string Value
        {
            get => _value;
            set
            {
                _value = value; 
                OnPropertyChanged();
            }
        }

        public virtual Task Task { get; set; }
    }
}