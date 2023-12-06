
using CleanArchitecture_Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace CleanArchitecture_Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
