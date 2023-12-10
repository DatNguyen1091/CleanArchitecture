
using Domain.Entities;

namespace CleanArchitecture_Application.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task<Category> GetCategoryWithProductEagerAsync(int id);
    }
}
