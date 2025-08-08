using Microsoft.EntityFrameworkCore;
using PMSAPI.Models;

namespace PMSAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
