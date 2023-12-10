
using Application.Dto;
using AutoMapper;
using CleanArchitecture_Application.Interfaces;
using MediatR;

namespace CleanArchitecture_Application.Features.Categories.Queries.GetById
{
    public class GetCategoryById : IRequest<CategoryDto>
    {
        public int CategoryId { get; set; }
    }
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetCategoryByIdHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }
    }
}
