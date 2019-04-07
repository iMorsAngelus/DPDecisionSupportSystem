using System;

namespace DecisionSupportSystem.DataAccessLayer.DataCreationModel
{
    public class AlternativePriority : BaseEntity
    {
        public Guid? TaskId { get; set; }
        public Guid? AlternativeId { get; set; }
        public Guid? CriteriaId { get; set; }
        public double Value { get; set; }
        
        public virtual Task Task { get; set; }
        public virtual Criteria Criteria { get; set; }
        public virtual Alternative Alternative { get; set; }
    }
}