using System;

namespace DecisionSupportSystem.DataAccessLayer.DataCreationModel
{
    public class CriteriaPriority : BaseEntity
    {
        public Guid TaskId { get; set; }
        public Guid CriteriaId { get; set; }
        public double Value { get; set; }

        public virtual Task Task { get; set; }
    }
}