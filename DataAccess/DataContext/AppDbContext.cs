using Microsoft.EntityFrameworkCore;
using OrchidPharmedApi.Entities;

namespace OrchidPharmedApi.DataAccess.DataContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<Entities.TaskEntity> TaskEntitys { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure any relationships, keys, or constraints here
        }
    }
}
