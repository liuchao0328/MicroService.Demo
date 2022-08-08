using SchoolManageMicroService.Classes.Models.Enums;

namespace SchoolManageMicroService.Classes.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public ClassType ClassType { get; set; }
    }
}
