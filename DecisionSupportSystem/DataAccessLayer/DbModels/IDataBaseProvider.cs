using System.Collections.ObjectModel;
using System.Linq;
using DecisionSupportSystem.DataAccessLayer.DataCreationModel;

namespace DecisionSupportSystem.DataAccessLayer.DbModels
{
    public interface IDataBaseProvider
    {
        Task CurrentTask { get; set; }
        IQueryable<Task> Tasks { get; }
        ObservableCollection<Task> ObservableTasks { get; }
        IQueryable<Alternative> Alternatives { get; }
        ObservableCollection<Alternative> ObservableAlternatives { get; }
        IQueryable<Criteria> Criterias { get; }
        ObservableCollection<Criteria> ObservableCriterias { get; }
        IQueryable<AlternativePriority> AlternativePriorityVectors { get; }
        IQueryable<CriteriaPriority> CriteriaPriorityVectors { get; }
        void SaveChanges();
        void RefreshData();
    }
}
