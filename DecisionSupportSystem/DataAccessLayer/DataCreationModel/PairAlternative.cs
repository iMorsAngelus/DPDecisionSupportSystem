using System;

namespace DecisionSupportSystem.DataAccessLayer.DataCreationModel
{
    public class PairAlternative : BaseEntity
    {
        public Guid TaskId { get; set; }
        public Guid FirstAlternativeId { get; set; }
        public Guid SecondAlternativeId { get; set; }
        public Guid CriteriaId { get; set; }
        public double MinRangeValue { get; set; }
        public double MiddleRangeValue { get; set; }
        public double MaxRangeValue { get; set; }

        public virtual Task Task { get; set; }
    }
}