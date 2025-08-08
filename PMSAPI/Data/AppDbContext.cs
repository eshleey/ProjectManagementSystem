using Microsoft.EntityFrameworkCore;
using PMSAPI.Models;

namespace PMSAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
