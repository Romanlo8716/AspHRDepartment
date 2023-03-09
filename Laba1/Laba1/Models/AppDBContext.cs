using Microsoft.EntityFrameworkCore;

namespace Laba1.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AdressDepartment> AdressDepartments { get; set; }

        public DbSet<Vacation> Vacations { get; set; }


        public DbSet<Education> Educations { get; set; }


    }
}
