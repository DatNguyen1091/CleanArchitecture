
using Application.Dto;
using AutoMapper;
using CleanArchitecture_Application.Features.Commands.Add;
using CleanArchitecture_Application.Features.Commands.Edit;
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
            CreateMap<CreateProduct, Product>().ReverseMap();
            CreateMap<UpdateProduct, Product>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<SignUp, ApplicationUser>().ReverseMap();
        }
    }
}
