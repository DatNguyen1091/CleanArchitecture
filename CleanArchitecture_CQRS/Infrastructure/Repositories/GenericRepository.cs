
using CleanArchitecture_Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture_Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);
            if (model == null)
            {
                return default(T)!;
            }

            return model;
        }

        public async Task<T> CreateAsync(T model)
        {
            await _context.Set<T>().AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<T> UpdateAsync(int id, T model)
        {
            var result = _context.Set<T>().Update(model);
            await _context.SaveChangesAsync();
            return result.Entity!;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);
            if (model != null)
            {
                _context.Set<T>().Remove(model);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
