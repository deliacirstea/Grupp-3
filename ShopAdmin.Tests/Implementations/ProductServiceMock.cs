using ShopGeneral.Infrastructure.Context;
using ShopGeneral.Services;

namespace ShopAdmin.Tests.Implementations
{
    public class ProductServiceMock : IProductService
    {

        public List<ProductServiceModel> ProductsToTest { get; set; } = new();

        public IEnumerable<ProductServiceModel> GetAllProducts() =>
            ProductsToTest;

        public IEnumerable<ProductServiceModel> GetNewProducts(int cnt, CurrentCustomerContext context) =>
            throw new NotImplementedException();

        public IEnumerable<ProductServiceModel> GetProductsByCategoryId(int categoryId) =>
            throw new NotImplementedException();
    }
}