using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DomainObjectsSalg.Sales;
using DomainObjectsSalg.Settings;


namespace CafeTerminal.DataAccess
{
    public class SalgDbContext : DbContext
    {

        public DbSet<Svinn> Svinn { get; set; }
        public DbSet<Vare> Varer { get; set; }
        public DbSet<Logg> Logg { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserLogg> UserLoggs { get; set; }
        public DbSet<Salg> Salg { get; set; }
        public DbSet<Settings> Settings { get; set; }


        public SalgDbContext()
            :base("SalgDatabase")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
