
using Application.Dto;
using AutoMapper;
using CleanArchitecture_Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace CleanArchitecture_Application.Features.Queries.GetById
{
    public class GetProductById : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
    }

    public class GetProductByIdHandler : IRequestHandler<GetProductById, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByIdHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }
    }
}
