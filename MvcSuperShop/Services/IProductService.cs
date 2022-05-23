using MvcSuperShop.Data;
using MvcSuperShop.Infrastructure.Context;

namespace MvcSuperShop.Services;

public interface IProductService
{
    public IEnumerable<ProductServiceModel> GetNewProducts(int cnt, CurrentCustomerContext context);
}