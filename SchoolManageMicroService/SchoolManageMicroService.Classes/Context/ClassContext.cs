using Microsoft.EntityFrameworkCore;
using SchoolManageMicroService.Classes.Models;
using System.Reflection;

namespace SchoolManageMicroService.Classes.Context
{
    public class ClassContext:DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public ClassContext(DbContextOptions<ClassContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
