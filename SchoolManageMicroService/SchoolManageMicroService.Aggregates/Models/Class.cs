using SchoolManageMicroService.Aggregates.Models.Enums;

namespace SchoolManageMicroService.Aggregates.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public ClassType ClassType { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
