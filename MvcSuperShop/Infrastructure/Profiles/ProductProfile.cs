using AutoMapper;
using MvcSuperShop.Data;
using MvcSuperShop.Services;
using MvcSuperShop.ViewModels;

namespace MvcSuperShop.Infrastructure.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductServiceModel, ProductBoxViewModel>();

        CreateMap<Product, ProductServiceModel>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.Category.Id))
            .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name));
    }
}