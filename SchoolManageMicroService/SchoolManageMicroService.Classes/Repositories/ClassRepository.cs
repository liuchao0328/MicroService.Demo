using Microsoft.EntityFrameworkCore;
using SchoolManageMicroService.Classes.Context;
using SchoolManageMicroService.Classes.Models;

namespace SchoolManageMicroService.Classes.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ClassContext classContext;

        public ClassRepository(ClassContext classContext)
        {
            this.classContext = classContext;
        }

        public async Task AddAsync(Class Class)
        {
            await classContext.Classes.AddAsync(Class);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var Class=await classContext.Classes.SingleOrDefaultAsync(c => c.Id==id);
            classContext.Classes.Remove(Class);
        }

        public Task<List<Class>> GetAllAsync()
        {
            return classContext.Classes.ToListAsync();
        }

        public Task<Class> GetByIdAsync(int id)
        {
            return classContext.Classes.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
