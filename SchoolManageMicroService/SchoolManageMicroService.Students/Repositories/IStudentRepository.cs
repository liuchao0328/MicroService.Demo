using SchoolManageMicroService.Students.Models;

namespace SchoolManageMicroService.Students.Repositories
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllAsync();
        public Task<Student> GetByIdAsync(int id);
        public Task DeleteByIdAsync(int id);
        public Task AddAsync(Student student);
        public Task<List<Student>> GetByClassIdAsync(int classId);
    }
}
