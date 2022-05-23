using MvcSuperShop.Infrastructure.Context;

namespace MvcSuperShop.Services;

public interface IPricingService
{
    IEnumerable<ProductServiceModel> CalculatePrices(IEnumerable<ProductServiceModel> products, CurrentCustomerContext context);
}