using FactoryAccessControl.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FactoryAccessControl.Infrastructure
{
    public class FactoryDb: DbContext
    {
        public FactoryDb(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Shift> Shifts => Set<Shift>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Внешний ключ без навигации
            modelBuilder.Entity<Shift>()
                .Property(s => s.EmployeeId)
                .IsRequired();
        }
        
    }
}
