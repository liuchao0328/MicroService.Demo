using Microsoft.EntityFrameworkCore;
using SchoolManageMicroService.Videos.Models;

namespace SchoolManageMicroService.Videos.Contexts
{
    public class VideoContext:DbContext
    {
        public VideoContext(DbContextOptions<VideoContext> options):base(options) { }
        public DbSet<Video> Videos { get; set; }
    }
}
