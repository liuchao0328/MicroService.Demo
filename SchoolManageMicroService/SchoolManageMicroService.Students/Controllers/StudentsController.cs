using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManageMicroService.Students.Services;

namespace SchoolManageMicroService.Students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var students = await studentService.GetAllAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route(nameof(GetByClassId))]
        public async Task<ActionResult> GetByClassId([FromRoute]int classId)
        {
            try
            {
                var students =await studentService.GetByClassIdAsync(classId);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
