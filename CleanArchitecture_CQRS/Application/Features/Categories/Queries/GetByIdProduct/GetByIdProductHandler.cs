
using AutoMapper;
using CleanArchitecture_Application.Dto;
using CleanArchitecture_Application.Interfaces;
using MediatR;

namespace CleanArchitecture_Application.Features.Categories.Queries.GetByIdProduct
{
    public class GetByIdProduct : IRequest<CategoryProductDto>
    {
        public int CategoryId { get; set; }
    }

    public class GetByIdProductHandler : IRequestHandler<GetByIdProduct, CategoryProductDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetByIdProductHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryProductDto> Handle(GetByIdProduct request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryWithProductEagerAsync(request.CategoryId);

            var categoryDto = _mapper.Map<CategoryProductDto>(category);

            return categoryDto;
        }
    }
}
