using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManageMicroService.Students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("心跳检查");
        }
    }
}
