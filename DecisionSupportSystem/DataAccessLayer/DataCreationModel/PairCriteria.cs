using System;

namespace DecisionSupportSystem.DataAccessLayer.DataCreationModel
{
    public class PairCriteria : BaseEntity
    {
        public Guid TaskId { get; set; }
        public Guid FirstCriteriaId { get; set; }
        public Guid SecondCriteriaId { get; set; }
        public double MinRangeValue { get; set; }
        public double MiddleRangeValue { get; set; }
        public double MaxRangeValue { get; set; }

        public virtual Task Task { get; set; }
    }
}