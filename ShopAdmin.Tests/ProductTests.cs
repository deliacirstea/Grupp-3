using Microsoft.Extensions.Logging;
using Moq;
using ShopAdmin.Commands;
using ShopGeneral.Infrastructure.Context;
using ShopGeneral.Services;

namespace ShopAdmin.Tests
{
    [TestClass]
    public class ProductTests
    {
        private Product sut;
        private readonly Mock<ILogger<Product>> loggerMock;
        private readonly IProductService productService;

        public ProductTests()
        {
            loggerMock = new Mock<ILogger<Product>>();
            productService = new ProductService();
            sut = new(loggerMock.Object, productService);
        }

        [TestMethod]
        public void OutputFileShouldBeGeneratedWhenExportIsCalled()
        {
            // TODO: Autofixture
            // Arrange
            string name = "test2";
            string filePath = @$"outfiles\{name}\{DateTime.UtcNow:yyyyMMdd}.txt";

            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            sut.export(name);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        public void Invalid_urls_should_be_logged_when_verify_image_is_called()
        {
            // Arrange
            sut = new(loggerMock.Object, new ProductService());
            string directoryPath = @"outfiles\products\";
            string filePath = $"{directoryPath}missingimages-{DateTime.UtcNow:yyyyMMdd}.txt";

            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            sut.verifyimage();

            // Assert
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        public void Correct_id_of_product_is_logged_when_having_invalid_url_when_verify_image_is_called()
        {
            // Arrange
            sut = new(loggerMock.Object, new ProductService());
            string directoryPath = @"outfiles\products\";
            string filePath = $"{directoryPath}missingimages-{DateTime.UtcNow:yyyyMMdd}.txt";

            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            sut.verifyimage();
            int resultId = Convert.ToInt32(File.ReadAllText(filePath));

            // Assert
            Assert.AreEqual(0, resultId);
        }

        [TestMethod]
        public void Valid_urls_should_not_be_logged_when_verify_image_is_called()
        {
            // Arrange
            sut = new(loggerMock.Object, new ProductService2());
            string directoryPath = @"outfiles\products\";
            string filePath = $"{directoryPath}missingimages-{DateTime.UtcNow:yyyyMMdd}.txt";

            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            sut.verifyimage();

            // Assert
            Assert.IsTrue(!File.Exists(filePath));
        }
    }

    public class ProductService : IProductService
    {
        public IEnumerable<ProductServiceModel> GetAllProducts()
        {
            // TODO: Use autofixture to populate a list of 100 objects
            return new List<ProductServiceModel>()
            {
                new ProductServiceModel() { Id = 0, Name = "test obj 1", ImageUrl = "http://invalid.jpg" }
            };
        }

        public IEnumerable<ProductServiceModel> GetNewProducts(int cnt, CurrentCustomerContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class ProductService2 : IProductService
    {
        public IEnumerable<ProductServiceModel> GetAllProducts()
        {
            // TODO: Use autofixture to populate a list of 100 objects
            return new List<ProductServiceModel>()
            {
                new ProductServiceModel() { Id = 0, Name = "test obj 1", ImageUrl = "https://www.google.com/search?q=dog&tbm=isch&sxsrf=APwXEdcKhQdruiF2CNyDfmD0MRYbUQH3BQ%3A1680510866105&source=hp&biw=1536&bih=718&ei=ko8qZJv_A_qJxc8Pt82nmAY&iflsig=AOEireoAAAAAZCqdonTLPZp208GmdbiM2gRis-eguRAR&ved=0ahUKEwibkNrppo3-AhX6RPEDHbfmCWMQ4dUDCAc&uact=5&oq=dog&gs_lcp=CgNpbWcQAzIFCAAQgAQyBQgAEIAEMgUIABCABDIFCAAQgAQyBQgAEIAEMgUIABCABDIFCAAQgAQyBQgAEIAEMgUIABCABDIFCAAQgAQ6BwgjEOoCECc6BAgjECdQkAxYuA5gwRBoAXAAeACAAViIAcABkgEBM5gBAKABAaoBC2d3cy13aXotaW1nsAEK&sclient=img#imgrc=PpmCvrB3OtU3hM"}
            };
        }

        public IEnumerable<ProductServiceModel> GetNewProducts(int cnt, CurrentCustomerContext context)
        {
            throw new NotImplementedException();
        }
    }
}