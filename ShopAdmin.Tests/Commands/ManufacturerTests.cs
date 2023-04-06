using Microsoft.Extensions.Logging;
using Moq;
using ShopAdmin.Commands;
using ShopAdmin.Tests.Implementations;

namespace ShopAdmin.Tests.Commands
{
    [TestClass]
    public class ManufacturerTests
    {
        private readonly Manufacturer sut;
        private readonly Mock<ILogger<Manufacturer>> loggerMock;
        private readonly EmailServiceMock emailServiceMock;

        public ManufacturerTests()
        {
            loggerMock = new Mock<ILogger<Manufacturer>>();
            emailServiceMock = new();
            sut = new(loggerMock.Object, emailServiceMock);
        }

        [TestMethod]
        public void Correct_Amount_Of_Emails_Is_Sent()
        {
            // Arrange
            emailServiceMock.EmailsToTest = new()
            {
               "aron@mail.com",
               "jacob@test.com",
               "dennis@test.com"
            };

            // Act
            sut.Sendreport();

            // Assert
            Assert.AreEqual(emailServiceMock.EmailsToTest.Count, emailServiceMock.SentMessages.Count);
        }

        [TestMethod]
        public void Emails_Is_Sent_To_The_Correct_Recipients()
        {
            // Arrange
            emailServiceMock.EmailsToTest = new()
            {
               "aron@mail.com",
               "jacob@test.com",
               "dennis@test.com"
            };

            // Act
            sut.Sendreport();

            // Assert
            for (int i = 0; i < emailServiceMock.EmailsToTest.Count; i++)
            {
                var message = emailServiceMock.SentMessages[i];

                foreach (var recipient in message.GetRecipients())
                    Assert.AreEqual(emailServiceMock.EmailsToTest[i], recipient.Address);
            }
        }

        [TestMethod]
        public void Correct_Email_Details_Is_Sent()
        {
            // Arrange
            emailServiceMock.EmailsToTest = new() { "aron@mail.com" };

            // Act
            sut.Sendreport();

            // Assert
            Assert.AreEqual("Test subject", emailServiceMock.SentMessages[0].Subject);
            Assert.AreEqual("Test body", emailServiceMock.SentMessages[0].TextBody);
            Assert.AreEqual("Test", emailServiceMock.SentMessages[0].From.Mailboxes.ElementAt(0).Name);
            Assert.AreEqual("tester@mvcsupershop.com", emailServiceMock.SentMessages[0].From.Mailboxes.ElementAt(0).Address);
        }
    }

}