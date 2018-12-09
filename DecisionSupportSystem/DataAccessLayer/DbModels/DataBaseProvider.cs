using System.Collections.ObjectModel;
using System.Linq;
using DecisionSupportSystem.DataAccessLayer.DataCreationModel;

namespace DecisionSupportSystem.DataAccessLayer.DbModels
{
    class DataBaseProvider : IDataBaseProvider
    {
        private readonly DecisionSupportSystemDataBaseModelContainer _context;

        public DataBaseProvider(DecisionSupportSystemDataBaseModelContainer context)
        {
            _context = context;
        }

        public IQueryable<Task> Tasks => _context.TaskSet;

        public ObservableCollection<Task> ObservableTasks => _context.TaskSet.Local;

        public IQueryable<Alternative> Alternatives => _context.AlternativeSet;
        public IQueryable<Criteria> Criterias => _context.CriteriaSet;
        public IQueryable<PairAlternative> PairAltternatives => _context.PairAlternativeSet;
        public IQueryable<AlternativePriorityVector> AlternativePriorityVectors => _context.AlternativePriorityVectorSet;
        public IQueryable<PairCriteria> PairCriterias => _context.PairCriteriaSet;
        public IQueryable<CriteriaPriorityVector> CriteriaPriorityVectors => _context.CriteriaPriorityVectorSet;

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}