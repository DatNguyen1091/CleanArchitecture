using Application.Dto;
using AutoMapper;
using CleanArchitecture_Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace CleanArchitecture_Application.Features.Categories.Commands.Edit
{

    public class UpdateCategory : IRequest<CategoryDto>
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }

        public UpdateCategory(int categoryId, string categoryName)
        {
            CategoryId = categoryId;
            Name = categoryName;
        }
    }

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategory, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryDto> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (category == null)
            {
                return default!;
            }

            var temp = _mapper.Map(request, category);
            var result = await _categoryRepository.UpdateAsync(request.CategoryId, temp);

            return _mapper.Map<CategoryDto>(result);
        }
    }
}
