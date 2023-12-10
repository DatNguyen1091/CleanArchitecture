
using CleanArchitecture_Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture_Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetCategoryWithProductEagerAsync(int id)
        {
            try
            {
                var categoryWithProduct = await _context.Categories!
                    .Where(x => x.CategoryId == id)
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync();

                if (categoryWithProduct == null)
                {
                    return null!;
                }

                return new Category
                {
                    CategoryId = categoryWithProduct.CategoryId,
                    Name = categoryWithProduct.Name,
                    Products = categoryWithProduct.Products!.ToList()
                };
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }
}
