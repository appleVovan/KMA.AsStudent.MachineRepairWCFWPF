using System.Data.Entity;
using MacineRepairTool.Models;

namespace MacineRepairTool.DBConnector
{
    internal class ModelsDbContext : DbContext
    {
        public ModelsDbContext() : base(@"server = DESKTOP-4496VDM\SQLEXPRESS; uid=sa;pwd=1;database=MachineRepairTool")
        {
            Database.SetInitializer<ModelsDbContext>(new CreateDatabaseIfNotExists<ModelsDbContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<MachineType> MachineTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RepairType> RepairTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User.UserConfiguration());
            modelBuilder.Configurations.Add(new Client.ClientConfiguration());
            modelBuilder.Configurations.Add(new Machine.MachineConfiguration());
            modelBuilder.Configurations.Add(new MachineType.MachineTypeConfiguration());
            modelBuilder.Configurations.Add(new Order.OrderConfiguration());
            modelBuilder.Configurations.Add(new RepairType.RepairTypeConfiguration());
        }
        
    }
}
