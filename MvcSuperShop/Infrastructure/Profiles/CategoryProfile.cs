using AutoMapper;
using MvcSuperShop.Data;
using MvcSuperShop.ViewModels;

namespace MvcSuperShop.Infrastructure.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryViewModel>();
    }
}