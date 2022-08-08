using SchoolManageMicroService.Classes.Models;
using SchoolManageMicroService.Classes.Repositories;

namespace SchoolManageMicroService.Classes.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository classRepository;

        public ClassService(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public async Task AddAsync(Class Class)
        {
           await classRepository.AddAsync(Class);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await classRepository.DeleteByIdAsync(id);
        }

        public Task<List<Class>> GetAllAsync()
        {
           return classRepository.GetAllAsync();
        }

        public Task<Class> GetByIdAsync(int id)
        {
            return classRepository.GetByIdAsync(id);
        }
    }
}
