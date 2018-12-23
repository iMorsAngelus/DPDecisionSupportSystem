using System.Data.Entity;

namespace DecisionSupportSystem.DataAccessLayer.DataCreationModel
{
    class DssContext : DbContext
    {
        public DssContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DssContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Task>()
                .HasMany(s => s.Criterias)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.TaskId);

            modelBuilder.Entity<Task>()
                .HasMany(s => s.Alternatives)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.TaskId);

            modelBuilder.Entity<Task>()
                .HasMany(s => s.AlternativePriorityVector)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.TaskId);

            modelBuilder.Entity<Task>()
                .HasMany(s => s.PairAlternatives)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.TaskId);

            modelBuilder.Entity<Task>()
                .HasMany(s => s.CriteriaPriorityVector)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.TaskId);

            modelBuilder.Entity<Task>()
                .HasMany(s => s.PairCriterias)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.TaskId);
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Alternative> Alternatives { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<AlternativePriority> AlternativePriorities { get; set; }
        public DbSet<CriteriaPriority> CriteriaPriorities { get; set; }
        public DbSet<PairAlternative> PairAlternatives { get; set; }
        public DbSet<PairCriteria> PairCriterias { get; set; }
    }
}
