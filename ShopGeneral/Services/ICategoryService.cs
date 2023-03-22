using ShopGeneral.Data;

namespace ShopGeneral.Services;

public interface ICategoryService
{
    IEnumerable<Category> GetTrendingCategories(int cnt);
}