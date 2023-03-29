using Microsoft.Extensions.Logging;
using ShopGeneral.Services;


namespace ShopAdmin.Commands
{
    public class Product : ConsoleAppBase
    {
        private readonly ILogger<Agreement> _logger;
        private readonly IPricingService _pricingService;

        public Product(ILogger<Product> logger, IPricingService pricingService)
        {
            _logger = logger;
            _pricingService = pricingService;
        }
        public void VerifyImages()
        {
            _logger.LogInformation("Expires starting");
            foreach (var image in _pricingService.)
            {

            }
            //foreach (var agreement in _agreementService.GetActiveAgreements()
            //             .Where(e => e.ValidTo < DateTime.Now.AddDays((days))))
            //{
            //    var expiresInDays = (agreement.ValidTo - DateTime.Now);
            //    Console.WriteLine($"{agreement.Id} expires in {expiresInDays} days");
            //}
            _logger.LogInformation("Expires ending");
        }
    }
}