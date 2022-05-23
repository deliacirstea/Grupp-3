using MvcSuperShop.Infrastructure.Context;

namespace MvcSuperShop.Services;

public class PricingService : IPricingService
{
    public IEnumerable<ProductServiceModel> CalculatePrices(IEnumerable<ProductServiceModel> products, CurrentCustomerContext context)
    {
        foreach (var product in products)
        {
            product.Price = product.BasePrice;
            yield return product;
        }
    }
}