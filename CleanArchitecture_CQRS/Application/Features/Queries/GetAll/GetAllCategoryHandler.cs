
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using CleanArchitecture_Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace CleanArchitecture_Application.Features.Queries.GetAll
{

    public class GetAllCategory : IRequest<ICollection<CategoryDto>>
    {

    }
    public class GetAllCategoryHandler : IRequestHandler<GetAllCategory, ICollection<CategoryDto>>
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public GetAllCategoryHandler(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<CategoryDto>> Handle(GetAllCategory request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoryDtos = _mapper.Map<ICollection<CategoryDto>>(categories);

            return categoryDtos;
        }
    }
}
