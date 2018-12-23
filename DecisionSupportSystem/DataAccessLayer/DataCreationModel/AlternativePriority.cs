using System;

namespace DecisionSupportSystem.DataAccessLayer.DataCreationModel
{
    public class AlternativePriority : BaseEntity
    {
        public Guid TaskId { get; set; }
        public Guid AlternativeId { get; set; }
        public string Value { get; set; }

        public virtual Task Task { get; set; }
    }
}