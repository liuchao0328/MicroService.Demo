using SchoolManageMicroService.Videos.Models;
using SchoolManageMicroService.Videos.Repositories;

namespace SchoolManageMicroService.Videos.Services
{
    public class VideoService : IVideoService
    {
        public readonly IVideoRepository VideoRepository;

        public VideoService(IVideoRepository VideoRepository)
        {
            this.VideoRepository = VideoRepository;
        }

        public void Create(Video Video)
        {
            VideoRepository.Create(Video);
        }

        public void Delete(Video Video)
        {
            VideoRepository.Delete(Video);
        }

        public Video GetVideoById(int id)
        {
            return VideoRepository.GetVideoById(id);
        }

        public IEnumerable<Video> GetVideos()
        {
            return VideoRepository.GetVideos();
        }

        public void Update(Video Video)
        {
            VideoRepository.Update(Video);
        }

        public bool VideoExists(int id)
        {
            return VideoRepository.VideoExists(id);
        }
    }
}
