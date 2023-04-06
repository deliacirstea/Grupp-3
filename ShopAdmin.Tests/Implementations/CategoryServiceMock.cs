using ShopGeneral.Services;

namespace ShopAdmin.Tests.Implementations
{
    public class CategoryServiceMock : ICategoryService
    {

        public List<ShopGeneral.Data.Category> CategoriesToTest { get; set; } = new();

        public IEnumerable<ShopGeneral.Data.Category> GetTrendingCategories(int cnt) =>
            CategoriesToTest;
    }
}