
namespace CleanArchitecture_Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<ICollection<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> CreateAsync(T model);
        public Task<T> UpdateAsync(int id, T model);
        public Task<int> DeleteAsync(int id);
    }
}
