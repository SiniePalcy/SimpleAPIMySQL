using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.DbContext
{
    public abstract class BaseDbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
    {
        private readonly string _dbConnectionString;

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }

        public BaseDbContext(IConfiguration configuration, string connectionStringName)
        {
            _dbConnectionString = configuration.GetConnectionString(connectionStringName)!;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_dbConnectionString is null)
            {
                throw new Exception("Connection string is not found in configuration");
            }

            optionsBuilder.UseMySQL(_dbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasMany(e => e.Employees)
                  .WithMany(emp => emp.Positions);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FIO).IsRequired();
                entity.HasMany(e => e.Positions)
                  .WithMany(p => p.Employees);
            });
        }

        public Task SaveAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
