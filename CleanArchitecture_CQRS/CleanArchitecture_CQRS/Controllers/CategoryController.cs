
using Application.Dto;
using CleanArchitecture_Application.Dto;
using CleanArchitecture_Application.Features.Categories.Commands.Add;
using CleanArchitecture_Application.Features.Categories.Commands.Delete;
using CleanArchitecture_Application.Features.Categories.Commands.Edit;
using CleanArchitecture_Application.Features.Categories.Queries.GetAll;
using CleanArchitecture_Application.Features.Categories.Queries.GetById;
using CleanArchitecture_Application.Features.Categories.Queries.GetByIdProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture_CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<CategoryDto>> GetCategoryListAsync()
        {
            var categories = await _mediator.Send(new GetAllCategory());

            return categories.ToList();
        }

        [HttpGet("{categoryId}")]
        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _mediator.Send(new GetCategoryById() { CategoryId = categoryId });

            return category!;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<CategoryDto> AddCategoryAsync(CategoryDto model)
        {
            var category = await _mediator.Send(new CreateCategory(
                model.Name!
            ));

            return category!;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{categoryId}")]
        public async Task<CategoryDto> UpdateCategoryAsync(int categoryId, CategoryDto model)
        {
            var category = await _mediator.Send(new UpdateCategory(
                model.CategoryId = categoryId,
                model.Name!
            ));

            return category!;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{categoryId}")]
        public async Task<int> DeleteCategoryAsync(int categoryId)
        {
            return await _mediator.Send(new DeleteCategory() { CategoryId = categoryId });
        }

        [HttpGet("Eager/{categoryId}")]
        public async Task<CategoryProductDto> GetCategoryByIdProductAsync(int categoryId)
        {
            var category = await _mediator.Send(new GetByIdProduct() { CategoryId = categoryId });

            return category!;
        }
    }
}