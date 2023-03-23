using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopGeneral.Services;

namespace ShopAdmin.Commands
{
    public class Agreement : ConsoleAppBase
    {
        private readonly ILogger<Agreement> _logger;
        private readonly IAgreementService _agreementService;

        public Agreement(ILogger<Agreement> logger, IAgreementService agreementService)
        {
            _logger = logger;
            _agreementService = agreementService;
        }
        public void Expires( int days)
        {
            _logger.LogInformation("Expires starting");
            foreach (var agreement in _agreementService.GetActiveAgreements()
                         .Where(e => e.ValidTo < DateTime.Now.AddDays((days))))
            {
                var expiresInDays = (agreement.ValidTo - DateTime.Now);
                Console.WriteLine($"{agreement.Id} expires in {expiresInDays} days");
            }
            _logger.LogInformation("Expires ending");
        }
    }
}
