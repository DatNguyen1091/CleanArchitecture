
using CleanArchitecture_Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace CleanArchitecture_Application.Features.Commands.Delete
{
    public class DeleteCategory : IRequest<int>
    {
        public int CategoryId { get; set; }
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategory, int>
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        public DeleteCategoryHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<int> Handle(DeleteCategory request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (category == null)
            {
                return default!;
            }

            return await _categoryRepository.DeleteAsync(category.CategoryId);
        }
    }
}
