using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManageMicroService.Videos.Models;
using SchoolManageMicroService.Videos.Services;

namespace SchoolManageMicroService.Videos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService videoService;

        public VideoController(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        // GET: api/Videos
        [HttpGet]
        public ActionResult<IEnumerable<Video>> GetVideos()
        {
            return videoService.GetVideos().ToList();
        }

        // GET: api/Videos/5
        [HttpGet("{id}")]
        public ActionResult<Video> GetVideo(int id)
        {
            Video Video = videoService.GetVideoById(id);

            if (Video == null)
            {
                return NotFound();
            }
            return Video;
        }

        // PUT: api/Videos/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public IActionResult PutVideo(int id, Video Video)
        {
            if (id != Video.Id)
            {
                return BadRequest();
            }

            try
            {
                videoService.Update(Video);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!videoService.VideoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// 视频添加(异步的操作)
        /// </summary>
        /// <param name="Video"></param>
        /// <returns></returns>


        /// 消费原理
        ///  1、AOP
        ///  2、RabbitMQ客户端
        ///  3、json序列化工具(将json反序列化)
        ///  订阅原理
        ///  1、RabbitMQ客户端 建立一个长连接


        /// 订阅模式扩展
        /// 一对一订阅
        /// 一对多订阅
        /// video.event video.event  video.event.1
        /// video.event video.1 video.1
        /// *  一对多匹配 消费者 video.*  生产者 video.1 video.2 video.1.1 video.1.1.1
        /// # 一对一匹配 消费者 video.#  生产者 video.1 video.2
        [NonAction]
        [CapSubscribe("video.url")]//参数 指定的消息队列
        public ActionResult<Video> PostVideo(Video Video)
        {
            // 如何实现方法幂等
            // 1、先记录当前方法的状态。未执行，成功，失败
            // 2、执行完成之后，更新状态，成功，失败

            // 如果再次消费，先判断状态，如果成功的，不进行业务操作
            // 如果失败的，就进行业务操作。字典需要全局
            // AOP思想

            // 1、阻塞30
            // Thread.Sleep(30000);
            //  throw new Exception("出现异常");
            Console.WriteLine($"接受到视频事件消息");
            videoService.Create(Video);
            return CreatedAtAction("GetVideo", new { id = Video.Id }, Video);
        }

        // DELETE: api/Videos/5
        [HttpDelete("{id}")]
        public ActionResult<Video> DeleteVideo(int id)
        {
            var Video = videoService.GetVideoById(id);
            if (Video == null)
            {
                return NotFound();
            }

            videoService.Delete(Video);
            return Video;
        }
    }
}
