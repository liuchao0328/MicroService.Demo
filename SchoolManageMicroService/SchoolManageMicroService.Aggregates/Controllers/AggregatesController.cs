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
        private readonly IClassesClientService _classesClientService;

        public AggregatesController(IClassesClientService classesClientService)
        {
            _classesClientService = classesClientService;
        }

        [HttpGet]
        [Route(nameof(GetClasses))]
        public async Task<ActionResult> GetClasses()
        {
            var classes = await _classesClientService.GetClasses();
            return Ok(classes);
        }
    }
}
