using SchoolManageMicroService.Classes.Models;

namespace SchoolManageMicroService.Classes.Services
{
    public interface IClassService
    {
        public Task<List<Class>> GetAllAsync();
        public Task<Class> GetByIdAsync(int id);
        public Task DeleteByIdAsync(int id);
        public Task AddAsync(Class Class);
    }
}
