using AutoMapper;
using MvcSuperShop.ViewModels;
using ShopGeneral.Data;
using ShopGeneral.Services;

namespace MvcSuperShop.Infrastructure.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductServiceModel, ProductBoxViewModel>();

    }
}