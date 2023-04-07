using MimeKit;
using ShopAdmin.Interfaces;

namespace ShopAdmin.Tests.Implementations
{
    public class EmailServiceMock : IEmailService
    {
        public List<string> EmailsToTest { get; set; } = new();

        public List<MimeMessage> SentMessages { get; private set; } = new();

        public void Authenticate() { }

        public void Connect() { }

        public void Disconnect() { }

        public List<string> GetEmails() => EmailsToTest;

        public TextPart GetMailBody(string recipient) =>
            new("plain") { Text = $"Test body" };

        public string GetMailSubject() => "Test subject";

        public MailboxAddress GetSenderMailAddress() =>
            new("Test", "tester@mvcsupershop.com");

        public void Send(MimeMessage message) =>
            SentMessages.Add(message);
    }
}