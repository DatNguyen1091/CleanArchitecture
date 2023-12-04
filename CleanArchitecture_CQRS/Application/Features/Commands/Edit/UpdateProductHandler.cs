﻿using Application.Dto;
using AutoMapper;
using CleanArchitecture_Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace CleanArchitecture_Application.Features.Commands.Edit
{

    public class UpdateProduct : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Quatity { get; set; }
        public int CategoryId { get; set; }

        public UpdateProduct(int productId, string productName, string productDescription, double productPrice, int productQuatity, int productCategoryId)
        {
            ProductId = productId;
            Name = productName;
            Description = productDescription;
            Price = productPrice;
            Quatity = productQuatity;
            CategoryId = productCategoryId;
        }
    }

    public class UpdateProductHandler : IRequestHandler<UpdateProduct, ProductDto>
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        public UpdateProductHandler(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(UpdateProduct request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (product == null)
            {
                return default!;
            }

            var temp = _mapper.Map(request, product);
            var result = await _productRepository.UpdateAsync(request.ProductId, temp);


            return _mapper.Map<ProductDto>(result);
        }
    }
}