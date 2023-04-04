using ShopGeneral.Infrastructure.Context;

namespace ShopGeneral.Services;

public interface IProductService
{
    public IEnumerable<ProductServiceModel> GetNewProducts(int cnt, CurrentCustomerContext context);

    public IEnumerable<ProductServiceModel> GetAllProducts(); // <-- Added so we can get all products

    public IEnumerable<ProductServiceModel> GetProductsByCategoryId(int categoryId); //<-- Added so we can sort products by each category
}

