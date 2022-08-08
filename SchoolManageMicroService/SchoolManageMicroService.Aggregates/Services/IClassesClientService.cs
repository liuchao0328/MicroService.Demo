using SchoolManageMicroService.Aggregates.Models;

namespace SchoolManageMicroService.Aggregates.Services
{
    public interface IClassesClientService
    {
        public Task<List<Class>> GetClasses();
    }
}
