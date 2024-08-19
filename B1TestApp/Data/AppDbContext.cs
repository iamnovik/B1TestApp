using System.Reflection;
using B1TestApp.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace B1TestApp.Data
{
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public DbSet<DataRecord> DataRecords { get; set; }
        
        public DbSet<BankAccountData> BankAccountsData { get; set; }
        
        public DbSet<IncomingBalance> IncomingBalances { get; set; }
        
        public DbSet<OutcomingBalance> OutcomingBalances { get; set; }
        
        public DbSet<Turnover> Turnovers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=B1Lines;Username=postgres;Password=postgres");
        }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

    
}