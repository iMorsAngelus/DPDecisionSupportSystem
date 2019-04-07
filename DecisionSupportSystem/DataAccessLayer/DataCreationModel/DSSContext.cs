using System;
using System.Data.Entity;

namespace DecisionSupportSystem.DataAccessLayer.DataCreationModel
{
    class DssContext : DbContext
    {
        public DssContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DssContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasMany(s => s.Criterias)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.TaskId)
                .WillCascadeOnDelete();

                modelBuilder.Entity<Task>()
                    .HasMany(s => s.Alternatives)
                    .WithRequired(e => e.Task)
                    .HasForeignKey(e => e.TaskId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Task>()
                .HasMany(s => s.AlternativePriorityVector)
                .WithOptional(e => e.Task)
                .HasForeignKey(e => e.TaskId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Task>()
                .HasMany(s => s.CriteriaPriorityVector)
                .WithOptional(e => e.Task)
                .HasForeignKey(e => e.TaskId)
                .WillCascadeOnDelete();
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Alternative> Alternatives { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<CriteriaPriority> CriteriaPriority { get; set; }
        public DbSet<AlternativePriority> AlternativePriority { get; set; }
    }
}
