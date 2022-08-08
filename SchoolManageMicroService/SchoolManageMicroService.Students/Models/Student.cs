using SchoolManageMicroService.Students.Models.Enums;

namespace SchoolManageMicroService.Students.Models
{
    public class Student
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 所属班级
        /// </summary>
        public int ClassId { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNumber { get; set; }
    }
}
