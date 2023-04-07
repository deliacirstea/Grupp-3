using MimeKit;

namespace ShopAdmin.Interfaces
{
    public interface IEmailService
    {
        public List<string> GetEmails();

        public void Send(MimeMessage message);

        public void Connect();

        public void Authenticate();

        public void Disconnect();

        public MailboxAddress GetSenderMailAddress();

        public string GetMailSubject();

        public TextPart GetMailBody(string recipient);

    }
}