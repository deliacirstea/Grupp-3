using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MvcSuperShop.Data;
using MvcSuperShop.Infrastructure.Context;

namespace MvcSuperShop.Services;

public class PricingService : IPricingService
{
    private readonly ApplicationDbContext _context;

    public PricingService(ApplicationDbContext context)
    {
        _context = context;
    }
    public IEnumerable<ProductServiceModel> CalculatePrices(IEnumerable<ProductServiceModel> products, CurrentCustomerContext customerContext)
    {
        foreach (var product in products)
        {
            var lowest = product.BasePrice;
            if (customerContext != null)
            {
                foreach (var agreement in customerContext.Agreements)
                {
                    foreach (var agreementRow in agreement.AgreementRows)
                    {
                        if (AgreementMatches(agreementRow, product))
                        {
                            var price = (1.0m - (agreementRow.PercentageDiscount / 100.0m)) * product.BasePrice;
                            if (price < lowest)
                                lowest = Convert.ToInt32(Math.Round(price, 0));
                        }

                    }
                }
            }
            product.Price = lowest;
            yield return product;
        }
    }

    private bool AgreementMatches(AgreementRow agreementRow, ProductServiceModel product)
    {
        var productCheck = !string.IsNullOrEmpty(agreementRow.ProductMatch);
        var categoryCheck = !string.IsNullOrEmpty(agreementRow.CategoryMatch);
        var manufacturerCheck = !string.IsNullOrEmpty(agreementRow.ManufacturerMatch);
        if (productCheck && !product.Name.ToLower().Contains(agreementRow.ProductMatch.ToLower()))
            return false;
        if (categoryCheck && !product.Name.ToLower().Contains(agreementRow.CategoryMatch.ToLower()))
            return false;
        if (manufacturerCheck && !product.Name.ToLower().Contains(agreementRow.ManufacturerMatch.ToLower()))
            return false;

        return true;

    }
}