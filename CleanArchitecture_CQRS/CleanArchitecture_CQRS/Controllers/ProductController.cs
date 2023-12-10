
using Application.Dto;
using CleanArchitecture_Application.Features.product.Commands.Add;
using CleanArchitecture_Application.Features.product.Commands.Delete;
using CleanArchitecture_Application.Features.product.Commands.Edit;
using CleanArchitecture_Application.Features.product.Queries.GetAll;
using CleanArchitecture_Application.Features.product.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture_CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ProductDto>> GetProductListAsync()
        {
            var products = await _mediator.Send(new GetAllProduct());

            return products.ToList();
        }

        [HttpGet("{productId}")]
        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product = await _mediator.Send(new GetProductById() { ProductId = productId });

            return product!;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ProductDto> AddProductAsync(ProductDto model)
        {
            var product = await _mediator.Send(new CreateProduct(
                model.Name!,
                model.Description!,
                model.Price,
                model.Quatity,
                model.CategoryId
            ));
            return product;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{productId}")]
        public async Task<ProductDto> UpdateProductAsync(int productId, ProductDto model)
        {
            var product = await _mediator.Send(new UpdateProduct(
                model.ProductId = productId,
                model.Name!,
                model.Description!,
                model.Price,
                model.Quatity,
                model.CategoryId
            ));
            return product;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{productId}")]
        public async Task<int> DeleteProductAsync(int productId)
        {
            return await _mediator.Send(new DeleteProduct() { ProductId = productId });
        }
    }
}
