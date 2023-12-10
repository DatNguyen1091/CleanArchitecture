
using Application.Dto;
using AutoMapper;
using CleanArchitecture_Application.Dto;
using CleanArchitecture_Application.Features.Categories.Commands.Add;
using CleanArchitecture_Application.Features.Categories.Commands.Edit;
using CleanArchitecture_Application.Features.product.Commands.Add;
using CleanArchitecture_Application.Features.product.Commands.Edit;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<CreateCategory, Category>().ReverseMap();
            CreateMap<UpdateCategory, Category>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryProductDto, Category>().ReverseMap();
            CreateMap<CreateProduct, Product>().ReverseMap();
            CreateMap<UpdateProduct, Product>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<SignUp, ApplicationUser>().ReverseMap();
        }
    }
}
