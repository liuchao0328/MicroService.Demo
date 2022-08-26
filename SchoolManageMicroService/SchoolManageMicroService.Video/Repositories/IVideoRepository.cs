using SchoolManageMicroService.Videos.Models;
namespace SchoolManageMicroService.Videos.Repositories
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetVideos();
       Video GetVideoById(int id);
        void Create(Video video);
        void Update(Video video);
        void Delete(Video video);
        bool VideoExists(int id);
    }
}
