using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SchoolManageMicroService.Students.Models;
using System.Reflection;

namespace SchoolManageMicroService.Students.Context
{
    public class StudentContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public StudentContext(DbContextOptions<StudentContext> Options) : base(Options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
