using MailKit.Net.Smtp;
using MimeKit;
using ShopAdmin.Interfaces;
using ShopGeneral.Data;

namespace ShopAdmin.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _mailClient;
        private readonly ApplicationDbContext _applicationDbContext;

        #region Customization

        private const string Host = "smtp.ethereal.email";
        private const int Port = 587;
        private const bool UseSsl = false;

        private const string Username = "charity.becker@ethereal.email";
        private const string Password = "VvM2d8t7KQ6sfYggqC";

        #endregion

        public EmailService(ApplicationDbContext applicationDbContext)
        {
            _mailClient = new();
            _applicationDbContext = applicationDbContext;
        }

        public void Authenticate() =>
            _mailClient.Authenticate(Username, Password);

        public void Connect() =>
            _mailClient.Connect(Host, Port, UseSsl);

        public void Disconnect() =>
            _mailClient.Disconnect(true);

        public List<string> GetEmails() =>
            _applicationDbContext.Manufacturers.Select(x => x.EmailReport).ToList();

        public void Send(MimeMessage message)
            => _mailClient.Send(message);

        public MailboxAddress GetSenderMailAddress() =>
            new("Salesreports", "robot@mvcsupershop.com");

        public string GetMailSubject() =>
            "Sales report";

        public TextPart GetMailBody(string recipient) =>
            new("plain") { Text = $"<Custom sales report to '{recipient}' here>" };

    }
}