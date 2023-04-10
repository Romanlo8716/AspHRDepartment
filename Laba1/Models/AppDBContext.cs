using Microsoft.EntityFrameworkCore;
using Laba1.Models;
using Laba1.Models.ViewModels;

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


        public DbSet<Laba1.Models.LaborBook>? LaborBook { get; set; }


        public DbSet<Laba1.Models.MedicalBook>? MedicalBook { get; set; }


        public DbSet<Laba1.Models.ViewModels.DescriptionsWorker>? DescriptionsWorker { get; set; }


        public DbSet<Laba1.Models.DepartmentsAndPostsOfWorker>? DepartmentsAndPostsOfWorker { get; set; }


     


    }
}
