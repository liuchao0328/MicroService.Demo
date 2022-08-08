using SchoolManageMicroService.Students.Models;

namespace SchoolManageMicroService.Students.Services
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllAsync();
        public Task<Student> GetByIdAsync(int id);
        public Task DeleteByIdAsync(int id);
        public Task AddAsync(Student student);
        public Task<List<Student>> GetByClassIdAsync(int classId);
    }
}
