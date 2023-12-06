using CleanArchitecture_Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace CleanArchitecture_Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
