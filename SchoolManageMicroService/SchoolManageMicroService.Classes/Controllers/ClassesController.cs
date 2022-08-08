using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManageMicroService.Classes.Services;

namespace SchoolManageMicroService.Classes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService classService;

        public ClassesController(IClassService classService)
        {
            this.classService = classService;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                Console.WriteLine("获取班级信息");
                var classes =await classService.GetAllAsync();
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

