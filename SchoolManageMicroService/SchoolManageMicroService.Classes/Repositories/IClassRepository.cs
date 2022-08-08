using SchoolManageMicroService.Classes.Models;

namespace SchoolManageMicroService.Classes.Repositories
{
    public interface IClassRepository
    {
        public Task<List<Class>> GetAllAsync();
        public Task<Class> GetByIdAsync(int id);
        public Task DeleteByIdAsync(int id);
        public Task AddAsync(Class Class);
    }
}
