using Microsoft.EntityFrameworkCore;
using SchoolManageMicroService.Students.Context;
using SchoolManageMicroService.Students.Models;

namespace SchoolManageMicroService.Students.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentContext studentContext;

        public StudentRepository(StudentContext studentContext)
        {
            this.studentContext = studentContext;
        }

        public async Task AddAsync(Student student)
        {
          await studentContext.Students.AddAsync(student);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var student =await studentContext.Students.SingleOrDefaultAsync(x => x.Id == id);
            studentContext.Students.Remove(student);
        }

        public Task<List<Student>> GetAllAsync()
        {
            return studentContext.Students.ToListAsync();
        }

        public Task<List<Student>> GetByClassIdAsync(int classId)
        {
            return studentContext.Students.Where(x => x.ClassId == classId).ToListAsync();
        }

        public Task<Student> GetByIdAsync(int id)
        {
            return studentContext.Students.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
