using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using DecisionSupportSystem.DataAccessLayer.DataCreationModel;

namespace DecisionSupportSystem.DataAccessLayer.DbModels
{
    class DataBaseProvider : IDataBaseProvider
    {
        private readonly DssContext _context;

        
        public DataBaseProvider(DssContext context)
        {
            _context = context;
            CurrentTask = Tasks.FirstOrDefault();
            RefreshData();
        }

        public void RefreshData()
        {
            _context.Tasks.Load();
            _context.Criterias.Load();
            _context.Alternatives.Load();
        }

        public Task CurrentTask { get; set; }
        public IQueryable<Task> Tasks => _context.Tasks;
        public ObservableCollection<Task> ObservableTasks => _context.Tasks.Local;
        public IQueryable<Alternative> Alternatives => _context.Alternatives;
        public ObservableCollection<Alternative> ObservableAlternatives => _context.Alternatives.Local;
        public IQueryable<Criteria> Criterias => _context.Criterias;
        public ObservableCollection<Criteria> ObservableCriterias => _context.Criterias.Local;
        public IQueryable<PairAlternative> PairAltternatives => _context.PairAlternatives;
        public IQueryable<AlternativePriority> AlternativePriorityVectors => _context.AlternativePriorities;
        public IQueryable<PairCriteria> PairCriterias => _context.PairCriterias;
        public IQueryable<CriteriaPriority> CriteriaPriorityVectors => _context.CriteriaPriorities;

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}