using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManageMicroService.Videos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        // GET: api/Teams
        [HttpGet]
        public ActionResult GetHealthCheck()
        {
            return Ok("连接正常");
        }
    }
}
