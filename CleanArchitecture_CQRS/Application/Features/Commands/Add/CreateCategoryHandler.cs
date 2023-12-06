
using MediatR;
using Application.Interfaces;
using Domain.Entities;
using AutoMapper;
using Application.Dto;
using CleanArchitecture_Application.Interfaces;

namespace CleanArchitecture_Application.Features.Commands.Add
{

    public class CreateCategory : IRequest<CategoryDto>
    {
        public string? Name { get; set; }
        public CreateCategory(string categoryName)
        {
            Name = categoryName;
        }
    }

    public class CreateCategoryHandler : IRequestHandler<CreateCategory, CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            var newCategory = _mapper.Map<Category>(request);
            var result = await _categoryRepository.CreateAsync(newCategory);

            return _mapper.Map<CategoryDto>(result);
        }
    }
}
