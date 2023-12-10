using Application.Dto;
using Domain.Entities;
using AutoMapper;
using CleanArchitecture_Application.Interfaces;
using MediatR;

namespace CleanArchitecture_Application.Features.product.Commands.Add
{
    public class CreateProduct : IRequest<ProductDto>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Quatity { get; set; }
        public int CategoryId { get; set; }

        public CreateProduct(string productName, string productDescription, double productPrice, int productQuatity, int productCategoryId)
        {
            Name = productName;
            Description = productDescription;
            Price = productPrice;
            Quatity = productQuatity;
            CategoryId = productCategoryId;
        }
    }

    public class CreateProductHandler : IRequestHandler<CreateProduct, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public string? Name { get; }
        public string? Description { get; }
        public double Price { get; }
        public int Quatity { get; }
        public int CategoryId { get; }

        public async Task<ProductDto> Handle(CreateProduct request, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Product>(request);
            var result = await _productRepository.CreateAsync(newProduct);

            return _mapper.Map<ProductDto>(result);
        }
    }
}
