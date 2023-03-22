using AutoMapper;
using MvcSuperShop.ViewModels;
using ShopGeneral.Data;

namespace MvcSuperShop.Infrastructure.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryViewModel>();
    }
}