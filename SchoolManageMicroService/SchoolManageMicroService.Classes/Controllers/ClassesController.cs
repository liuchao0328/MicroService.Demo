using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManageMicroService.Classes.Models;
using SchoolManageMicroService.Classes.Models.Enums;
using SchoolManageMicroService.Classes.Services;

namespace SchoolManageMicroService.Classes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService classService;
        private readonly IConfiguration configuration;
        public ClassesController(IClassService classService, IConfiguration configuration)
        {
            this.classService = classService;
            this.configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                //Console.WriteLine("获取班级信息");
                //bool flag = Convert.ToBoolean(configuration["flag"]);
                //List<Class> classes = new List<Class>();
                /////服务降级
                //if (!flag)
                //{
                //    classes = await classService.GetAllAsync();
                //}
                //else
                //{
                //    classes = new List<Class>();
                //}
                var classes = new List<Class>()
               {
                   new Class()
                   {
                       Id=1,
                       ClassName="软件工程七班",
                       ClassType=ClassType.LiberalArts
                   },
                   new Class()
                   {
                       Id=2,
                       ClassName="软件工程八班",
                       ClassType=ClassType.LiberalArts
                   }
               };
                return Ok(classes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            try
            {
                var Class = await classService.GetByIdAsync(id);
                return Ok(Class);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

