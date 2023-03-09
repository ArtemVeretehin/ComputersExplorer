using Microsoft.EntityFrameworkCore;
using ComputersExplorer.DBO;
namespace ComputersExplorer
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class ComputersExplorerContext: DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Computer> Computers { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        public ComputersExplorerContext(DbContextOptions<ComputersExplorerContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                    new Role { Id = 1, Name = "Admin"},
                    new Role { Id = 2, Name = "User" }
            );
        }
    }
}
