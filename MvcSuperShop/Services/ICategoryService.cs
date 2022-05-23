using MvcSuperShop.Data;

namespace MvcSuperShop.Services;

public interface ICategoryService
{
    IEnumerable<Category> GetTrendingCategories(int cnt);
}