using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolManageMicroService.Aggregates.Models;
using SchoolManageMicroService.Aggregates.Services;

namespace SchoolManageMicroService.Aggregates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AggregatesController : ControllerBase
    {
        //private readonly IHttpClientFactory httpClientFactory;

        //public AggregatesController(IHttpClientFactory httpClientFactory)
        //{
        //    this.httpClientFactory = httpClientFactory;
        //}
        private readonly ILogger<AggregatesController> logger;
        private readonly ICapPublisher publisher;
        private readonly IClassesClientService _classesClientService;

        public AggregatesController(IClassesClientService classesClientService, ICapPublisher publisher, ILogger<AggregatesController> logger)
        {
            _classesClientService = classesClientService;
            this.publisher = publisher;
            this.logger = logger;
        }

        [HttpGet]
        [Route(nameof(GetClasses))]
        public async Task<ActionResult> GetClasses()
        {
            var classes = await _classesClientService.GetClasses();
            return Ok(classes);
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            Video video = new Video()
            {
                VideoUrl = "http://localhost:8888/1232133321",
                MemberId = 1
            };
            publisher.Publish<Video>("video.url", video);
            return Ok("添加成功");
        }


        [HttpGet]
        public async Task<ActionResult> GetById(int id)
        {
            logger.LogInformation("获取数据啦啦啦啦all，这里调用接口拉啊啦啦啦");
            logger.LogWarning("注意注意这里可能会出现bug，注意注意这里可能会出现bug");
            logger.LogError("出现异常信息拉，警报系统出现bug了系统出现bug了");
            return Ok("哈哈");
        }
    }
}
