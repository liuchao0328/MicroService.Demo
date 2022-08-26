using SchoolManageMicroService.Videos.Models;

namespace SchoolManageMicroService.Videos.Services
{
    public interface IVideoService
    {
        IEnumerable<Video> GetVideos();
        Video GetVideoById(int id);
        void Create(Video video);
        void Update(Video video);
        void Delete(Video video);
        bool VideoExists(int id);
    }
}
