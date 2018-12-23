using System;
using System.ComponentModel.DataAnnotations.Schema;
using DecisionSupportSystem.PresentationLayer;

namespace DecisionSupportSystem.DataAccessLayer.DataCreationModel
{
    public abstract class BaseEntity : NotifyPropertyChanged
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
    }
}