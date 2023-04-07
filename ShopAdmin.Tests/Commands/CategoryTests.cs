using Microsoft.Extensions.Logging;
using Moq;
using ShopAdmin.Commands;
using ShopAdmin.Tests.Implementations;

namespace ShopAdmin.Tests.Commands
{
    [TestClass]
    public class CategoryTests
    {
        private readonly Category sut;
        private readonly Mock<ILogger<Category>> _loggerMock;
        private readonly CategoryServiceMock _categoryServiceMock;
        private readonly ProductServiceMock _productServiceMock;

        public CategoryTests()
        {
            _loggerMock = new Mock<ILogger<Category>>();
            _categoryServiceMock = new();
            _productServiceMock = new();
            sut = new(_loggerMock.Object, _categoryServiceMock, _productServiceMock);
        }

        [TestMethod]
        public void Categories_that_are_linked_to_products_should_not_be_logged()
        {
            // Arrange
            _productServiceMock.ProductsToTest = new()
            {
                new() { Id = 0, Name = "Test dog 1", CategoryId = 0 },
                new() { Id = 1, Name = "Test dog 2", CategoryId = 1 },
                new() { Id = 2, Name = "Test dog 3", CategoryId = 2 }
            };

            _categoryServiceMock.CategoriesToTest = new()
            {
                new() { Id = 0, Name = "Test kat 1", Icon = "" },
                new() { Id = 1, Name = "Test kat 2", Icon = "" },
                new() { Id = 2, Name = "Test kat 3", Icon = "" },
            };

            string directoryPath = "outfiles\\category\\";
            string filePath = $"{directoryPath}missingproducts-{DateTime.UtcNow:yyyyMMdd}.txt";

            // Make sure that there are no log file to begin with
            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            sut.Checkempty();

            // Assert
            Assert.IsFalse(File.Exists(filePath));
        }

        [TestMethod]
        public void Categories_that_are_not_linked_to_products_should_be_logged()
        {
            //arrange
            _productServiceMock.ProductsToTest = new()
            {
                new() { Id = 0, Name = "Test dog 1", CategoryId = 4 },
                new() { Id = 1, Name = "Test dog 2", CategoryId = 5 },
                new() { Id = 2, Name = "Test dog 3", CategoryId = 3 }
            };

            _categoryServiceMock.CategoriesToTest = new()
            {
                new() { Id = 0, Name = "Test kat 1", Icon = "" },
                new() { Id = 1, Name = "Test kat 2", Icon = "" },
                new() { Id = 2, Name = "Test kat 3", Icon = "" },
            };
            string directoryPath = "outfiles\\category\\";
            string filePath = $"{directoryPath}missingproducts-{DateTime.UtcNow:yyyyMMdd}.txt";

            if (File.Exists(filePath)) File.Delete(filePath);
            //act
            sut.Checkempty();
            //assert
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        public void Correct_Category_Name_Is_Logged()
        {
            // Arrange
            _productServiceMock.ProductsToTest = new()
            {
                new() { Id = 0, Name = "Test dog 1", CategoryId = 4 },
                new() { Id = 1, Name = "Test dog 2", CategoryId = 5 },
                new() { Id = 2, Name = "Test dog 3", CategoryId = 6 }
            };

            _categoryServiceMock.CategoriesToTest = new()
            {
                new() { Id = 0, Name = "Test kat 1", Icon = "" },
                new() { Id = 1, Name = "Test kat 2", Icon = "" },
                new() { Id = 2, Name = "Test kat 3", Icon = "" },
            };

            string directoryPath = "outfiles\\category\\";
            string filePath = $"{directoryPath}missingproducts-{DateTime.UtcNow:yyyyMMdd}.txt";

            // Make sure that there are no log file to begin with
            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            sut.Checkempty();

            // Assert
            Assert.IsTrue(File.Exists(filePath));

            int index = 0;
            foreach (string line in File.ReadLines(filePath))
                Assert.AreEqual(_categoryServiceMock.CategoriesToTest[index++].Name, line);
        }
    }

}