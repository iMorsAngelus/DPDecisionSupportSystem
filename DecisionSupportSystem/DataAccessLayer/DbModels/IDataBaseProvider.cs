using System.Collections.ObjectModel;
using System.Linq;
using DecisionSupportSystem.DataAccessLayer.DataCreationModel;

namespace DecisionSupportSystem.DataAccessLayer.DbModels
{
    public interface IDataBaseProvider
    {
        IQueryable<Task> Tasks { get; }
        ObservableCollection<Task> ObservableTasks { get; }
        IQueryable<Alternative> Alternatives { get; }
        IQueryable<Criteria> Criterias { get; }
        IQueryable<PairAlternative> PairAltternatives { get; }
        IQueryable<AlternativePriorityVector> AlternativePriorityVectors { get; }
        IQueryable<PairCriteria> PairCriterias { get; }
        IQueryable<CriteriaPriorityVector> CriteriaPriorityVectors { get; }
        void SaveChanges();
    }
}
