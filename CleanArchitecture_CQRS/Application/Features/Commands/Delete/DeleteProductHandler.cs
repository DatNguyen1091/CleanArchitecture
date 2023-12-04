﻿
using CleanArchitecture_Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace CleanArchitecture_Application.Features.Commands.Delete
{
    public class DeleteProduct : IRequest<int>
    {
        public int ProductId { get; set; }
    }

    public class DeleteProductHandler : IRequestHandler<DeleteProduct, int>
    {
        private readonly IGenericRepository<Product> _productRepository;
        public DeleteProductHandler(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(DeleteProduct request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (product == null)
            {
                return default!;
            }

            return await _productRepository.DeleteAsync(product.ProductId);
        }
    }
}
