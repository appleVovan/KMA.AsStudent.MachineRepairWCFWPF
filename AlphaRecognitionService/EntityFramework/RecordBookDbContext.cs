using System.Data.Entity;

namespace MachineRepair.EntityFramework
{
    internal class RecordBookDbContext : DbContext
    {
        public RecordBookDbContext() : base(@"server = DESKTOP-4496VDM\SQLEXPRESS; uid=sa;pwd=1;database=RecordBook")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<RecordBookDbContext>());
        }

        public DbSet<DateModel> DateModels { get; set; }
        public DbSet<EventModel> EventModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DateModel.DateModelConfiguration());
            modelBuilder.Configurations.Add(new EventModel.EventModelConfiguration());
        }
        
    }
}
