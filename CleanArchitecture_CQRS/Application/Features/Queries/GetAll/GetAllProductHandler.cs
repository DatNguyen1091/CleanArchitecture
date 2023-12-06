using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using CleanArchitecture_Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace CleanArchitecture_Application.Features.Queries.GetAll
{

    public class GetAllProduct : IRequest<ICollection<ProductDto>>
    {

    }

    public class GetAllProductHandler : IRequestHandler<GetAllProduct, ICollection<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<ProductDto>> Handle(GetAllProduct request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            var productDtos = _mapper.Map<ICollection<ProductDto>>(products);

            return productDtos;
        }
    }
}
