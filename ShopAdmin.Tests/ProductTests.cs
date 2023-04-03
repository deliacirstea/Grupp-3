using Bogus.Bson;
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
        private readonly Product sut;
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
            string filePath = @$"C:\Users\newsi\source\repos\KyhTestingStartingCaseTuc3\ShopAdmin.Tests\bin\Debug\net7.0\outfiles\{name}\{DateTime.UtcNow:yyyyMMdd}.txt";

            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            sut.export(name);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
        }
    }

    public class ProductService : IProductService
    {
        public IEnumerable<ProductServiceModel> GetAllProducts()
        {
            // TODO: Use autofixture to populate a list of 100 objects
            return new List<ProductServiceModel>()
            {
                new ProductServiceModel() { Id = 0, Name = "test obj 1"}
            };
        }

        public IEnumerable<ProductServiceModel> GetNewProducts(int cnt, CurrentCustomerContext context)
        {
            throw new NotImplementedException();
        }
    }
}