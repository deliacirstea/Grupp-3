using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using ShopGeneral.Data;

namespace ShopAdmin.Commands
{
    public class Manufacturer : ConsoleAppBase
    {
        private readonly ILogger<Manufacturer> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly SmtpClient _mailClient; 

        public Manufacturer(ILogger<Manufacturer> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _mailClient = new();
        }

        public void Sendreport()
        {
            _logger.LogInformation("SendReport starting");

            List<string> emails = _applicationDbContext.Manufacturers.Select(x => x.EmailReport).ToList();

            _mailClient.Connect("smtp.ethereal.email", 587, false);
            _mailClient.Authenticate("charity.becker@ethereal.email", "VvM2d8t7KQ6sfYggqC");
            
            MailboxAddress fromAddress = new MailboxAddress("Dennis Hankvist", "dennis@test.com");
            foreach (var email in emails)
            {
                MimeMessage message = new()
                {
                    Subject = "Test subject",
                    Body = new TextPart("plain") { Text = "Hello" },
                };

                string mailTo = email.Replace(" ", "");
                message.To.Add(new MailboxAddress(mailTo, mailTo));
                message.From.Add(fromAddress);

                _mailClient.Send(message);
            }

            _mailClient.Disconnect(true);

            _logger.LogInformation("SendReport ending");
        }
        
    }

    public interface IEmailService
    {
        public List<string> GetEmails();

        public void Send();

    }

}
