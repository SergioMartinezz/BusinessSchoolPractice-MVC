using EscuelaNegociosSergio.Models;
using Microsoft.EntityFrameworkCore;

namespace EscuelaNegociosSergio.Context
{
    public class SchoolDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {

        }
    }
}
