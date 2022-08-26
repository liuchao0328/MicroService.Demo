using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SchoolManageMicroService.Videos.Contexts
{
    public class VideoContextFactory : IDesignTimeDbContextFactory<VideoContext>
    {
        public VideoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoContext>();
            optionsBuilder.UseMySql("server=localhost;port=3306;database=School_Videos;uid=root;pwd=123456;CharSet=utf8", new MySqlServerVersion(new Version(8, 0, 27)));
            return new VideoContext(optionsBuilder.Options);
        }
    }
}
