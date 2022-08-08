using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManageMicroService.Classes.Models;

namespace SchoolManageMicroService.Classes.Configs
{
    public class ClassConfig : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.Property(x => x.ClassType).IsUnicode().HasMaxLength(20).HasConversion<string>();
        }
    }
}
