using SchoolManageMicroService.Videos.Contexts;
using SchoolManageMicroService.Videos.Models;

namespace SchoolManageMicroService.Videos.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        public VideoContext videoContext;
        public VideoRepository(VideoContext videoContext)
        {
            this.videoContext = videoContext;
        }
        public void Create(Video video)
        {
            videoContext.Videos.Add(video);
            videoContext.SaveChanges();
        }

        public void Delete(Video video)
        {
            videoContext.Videos.Remove(video);
            videoContext.SaveChanges();
        }

        public Video GetVideoById(int id)
        {
            return videoContext.Videos.Find(id);
        }

        public IEnumerable<Video> GetVideos()
        {
            return videoContext.Videos.ToList();
        }

        public void Update(Video video)
        {
            videoContext.Videos.Update(video);
            videoContext.SaveChanges();
        }
        public bool VideoExists(int id)
        {
            return videoContext.Videos.Any(e => e.Id == id);
        }
    }
}
