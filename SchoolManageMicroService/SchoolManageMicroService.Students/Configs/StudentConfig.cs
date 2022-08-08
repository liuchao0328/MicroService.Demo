using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManageMicroService.Students.Models;

namespace SchoolManageMicroService.Students.Configs
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //实现值对象
            builder.Property(x => x.Gender).IsUnicode().HasMaxLength(10).HasConversion<string>();
        }
    }
}
