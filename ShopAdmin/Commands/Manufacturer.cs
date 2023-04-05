using Microsoft.Extensions.Logging;
using MimeKit;
using ShopAdmin.Interfaces;

namespace ShopAdmin.Commands
{
    public class Manufacturer : ConsoleAppBase
    {
        private readonly ILogger<Manufacturer> _logger;
        private readonly IEmailService _emailService;

        public Manufacturer(ILogger<Manufacturer> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public void Sendreport()
        {
            _logger.LogInformation("SendReport starting");

            // Do not send a report if its not the 3rd day of the month
            // Note: this logic should not be in here but in the calling method
            //if (DateTime.UtcNow.Day != 3)
            //{
            //    _logger.LogInformation("Mail reports is not supposed to be sent this day. Exiting method.");
            //    return;
            //}

            List<string> emails = _emailService.GetEmails();

            _emailService.Connect();
            _emailService.Authenticate();

            // Create and send mails to every manufacturer
            MailboxAddress fromEmailAddress = _emailService.GetSenderMailAddress();
            foreach (var email in emails)
            {
                // Make sure that there are no spaces in the mail address
                string recipientEmailAddress = email.Replace(" ", "");

                // Create a new emailMessage
                MimeMessage emailMessage = new()
                {
                    Subject = _emailService.GetMailSubject(),
                    Body = _emailService.GetMailBody(recipientEmailAddress),
                };

                emailMessage.To.Add(new MailboxAddress(recipientEmailAddress, recipientEmailAddress));
                emailMessage.From.Add(fromEmailAddress);

                _emailService.Send(emailMessage);
            }

            _emailService.Disconnect();

            _logger.LogInformation("SendReport ending");
        }

    }

}
