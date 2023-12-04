
using Application.Dto;
using CleanArchitecture_Application.Features.Commands.Add;
using CleanArchitecture_Application.Features.Commands.Delete;
using CleanArchitecture_Application.Features.Commands.Edit;
using CleanArchitecture_Application.Features.Queries.GetAll;
using CleanArchitecture_Application.Features.Queries.GetById;
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

        //[Authorize]
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

        //[Authorize]
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

        //[Authorize]
        [HttpDelete("{productId}")]
        public async Task<int> DeleteProductAsync(int productId)
        {
            return await _mediator.Send(new DeleteProduct() { ProductId = productId });
        }
    }
}
