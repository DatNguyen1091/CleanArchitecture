
using Domain.Entities;

namespace CleanArchitecture_Application.Dto
{
    public class CategoryProductDto
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
