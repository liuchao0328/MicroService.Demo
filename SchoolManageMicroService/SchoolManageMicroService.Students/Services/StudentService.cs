using SchoolManageMicroService.Students.Models;
using SchoolManageMicroService.Students.Repositories;

namespace SchoolManageMicroService.Students.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public async Task AddAsync(Student student)
        {
         await studentRepository.AddAsync(student);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await studentRepository.DeleteByIdAsync(id);
        }

        public Task<List<Student>> GetAllAsync()
        {
            return studentRepository.GetAllAsync();
        }

        public Task<List<Student>> GetByClassIdAsync(int classId)
        {
            return studentRepository.GetByClassIdAsync(classId);
        }

        public Task<Student> GetByIdAsync(int id)
        {
            return studentRepository.GetByIdAsync(id);
        }
    }
}
