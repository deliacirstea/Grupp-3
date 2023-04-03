using Microsoft.Extensions.Logging;
using Moq;
using ShopAdmin.Commands;
using ShopGeneral.Infrastructure.Context;
using ShopGeneral.Services;
using System.Text.Json;

namespace ShopAdmin.Tests
{
    [TestClass]
    public class ProductTests
    {
        private readonly Product sut;
        private readonly Mock<ILogger<Product>> loggerMock;
        private readonly ProductService productService;

        public ProductTests()
        {
            loggerMock = new Mock<ILogger<Product>>();
            productService = new ProductService();
            sut = new(loggerMock.Object, productService);
        }

        [TestMethod]
        public void Output_File_Should_Be_Generated_When_Export_Is_Called()
        {
            // Arrange
            productService.Items = CreateDefaultExportItems();
            string name = "export_file";
            string filePath = @$"outfiles\{name}\{DateTime.UtcNow:yyyyMMdd}.txt";

            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            sut.export(name);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        public void When_Using_Export_Skip_Should_Return_0()
        {
            // Arrange
            productService.Items = CreateDefaultExportItems();
            string name = "export_skip";
            string filePath = @$"outfiles\{name}\{DateTime.UtcNow:yyyyMMdd}.txt";

            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            sut.export(name);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                ExportProduct test = JsonSerializer.Deserialize<ExportProduct>(json)!;
                Assert.AreEqual(0, test.skip);
            }
        }

        [TestMethod]
        public void When_Using_Export_Limit_Should_Return_2()
        {
            //ARRANGE   
            productService.Items = CreateDefaultExportItems();
            string name = "export_limit";
            string filePath = @$"outfiles\{name}\{DateTime.UtcNow:yyyyMMdd}.txt";

            if (File.Exists(filePath)) File.Delete(filePath);

            //ACT
            sut.export(name);

            //Assert
            Assert.IsTrue(File.Exists(filePath));
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                ExportProduct test = JsonSerializer.Deserialize<ExportProduct>(json)!;
                Assert.AreEqual(2, test.limit);
            }
        }

        [TestMethod]
        public void When_Using_Export_Total_Should_Return_2()
        {
            //ARRANGE   
            productService.Items = CreateDefaultExportItems();
            string name = "export_total";
            string filePath = @$"outfiles\{name}\{DateTime.UtcNow:yyyyMMdd}.txt";

            //ACT
            sut.export(name);

            //Assert
            Assert.IsTrue(File.Exists(filePath));
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                ExportProduct test = JsonSerializer.Deserialize<ExportProduct>(json)!;
                Assert.AreEqual(2, test.total);
            }
        }

        [TestMethod]
        public void When_Using_Export_All_Properties_Of_Products_Should_Be_Included()
        {
            //ARRANGE   
            productService.Items = CreateDefaultExportItems();
            string name = "export_properties";
            string filePath = @$"outfiles\{name}\{DateTime.UtcNow:yyyyMMdd}.txt";

            //ACT
            sut.export(name);

            //Assert
            Assert.IsTrue(File.Exists(filePath));
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                ExportProduct test = JsonSerializer.Deserialize<ExportProduct>(json)!;
                Assert.AreEqual(0, test.products[0].ID);
                Assert.AreEqual("test obj 1", test.products[0].Title);
                Assert.AreEqual("http://invalid.jpg", test.products[0].Image);
                Assert.AreEqual(100, test.products[0].Price);
                Assert.AreEqual("man 1", test.products[0].Brand);
                Assert.AreEqual("kat 1", test.products[0].Category);
            }
        }

        [TestMethod]
        public void Invalid_urls_should_be_logged_when_verify_image_is_called()
        {
            // Arrange
            productService.Items = CreateDefaultVerifyImageItems();

            string directoryPath = @"outfiles\products\";
            string filePath = $"{directoryPath}missingimages-{DateTime.UtcNow:yyyyMMdd}.txt";
            sut.verifyimage();

            // Assert
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        public void Valid_urls_should_not_be_logged_when_verify_image_is_called()
        {
            // Arrange
            productService.Items = new List<ProductServiceModel>()
            {
                new ProductServiceModel() {
                    Id = 0,
                    Name = "test obj 1",
                    ImageUrl = "https://www.google.com/search?q=dog&tbm=isch&sxsrf=APwXEdcKhQdruiF2CNyDfmD0MRYbUQH3BQ%3A1680510866105&source=hp&biw=1536&bih=718&ei=ko8qZJv_A_qJxc8Pt82nmAY&iflsig=AOEireoAAAAAZCqdonTLPZp208GmdbiM2gRis-eguRAR&ved=0ahUKEwibkNrppo3-AhX6RPEDHbfmCWMQ4dUDCAc&uact=5&oq=dog&gs_lcp=CgNpbWcQAzIFCAAQgAQyBQgAEIAEMgUIABCABDIFCAAQgAQyBQgAEIAEMgUIABCABDIFCAAQgAQyBQgAEIAEMgUIABCABDIFCAAQgAQ6BwgjEOoCECc6BAgjECdQkAxYuA5gwRBoAXAAeACAAViIAcABkgEBM5gBAKABAaoBC2d3cy13aXotaW1nsAEK&sclient=img#imgrc=PpmCvrB3OtU3hM"
                }
            };

            string directoryPath = @"outfiles\products\";
            string filePath = $"{directoryPath}missingimages-{DateTime.UtcNow:yyyyMMdd}.txt";

            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            sut.verifyimage();

            // Assert
            Assert.IsTrue(!File.Exists(filePath));
        }

        [TestMethod]
        public void Correct_id_of_product_is_logged_when_having_invalid_url_when_verify_image_is_called()
        {
            // Arrange
            productService.Items = CreateDefaultVerifyImageItems();
            string directoryPath = @"outfiles\products\";
            string filePath = $"{directoryPath}missingimages-{DateTime.UtcNow:yyyyMMdd}.txt";

            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            sut.verifyimage();
            int resultId = Convert.ToInt32(File.ReadAllText(filePath));

            // Assert
            Assert.AreEqual(0, resultId);
        }

        private static List<ProductServiceModel> CreateDefaultExportItems() => new () 
        {
            new ProductServiceModel(){
                Id = 0,
                Name = "test obj 1",
                ImageUrl = "http://invalid.jpg",
                AddedUtc = DateTime.UtcNow,
                BasePrice = 100,
                CategoryId = 0,
                CategoryName = "kat 1",
                ManufacturerId = 0,
                ManufacturerName = "man 1",
                Price = 10
            },
            new ProductServiceModel()
            {
                Id = 1,
                Name = "test obj 2",
                ImageUrl = "http://invalid.jpg",
                AddedUtc = DateTime.UtcNow,
                BasePrice = 100,
                CategoryId = 1,
                CategoryName = "kat 2",
                ManufacturerId = 1,
                ManufacturerName = "man 2",
                Price = 10
            }
        };

        private static List<ProductServiceModel> CreateDefaultVerifyImageItems() => new () 
        {
            new ProductServiceModel() {
                Id = 0,
                Name = "test obj 1",
                ImageUrl = "http://invalid.jpg"
            }
        };
    }


    public class ProductService : IProductService
    {
        public List<ProductServiceModel> Items { get; set; } = new();

        public IEnumerable<ProductServiceModel> GetAllProducts() => Items;

        public IEnumerable<ProductServiceModel> GetNewProducts(int cnt, CurrentCustomerContext context) => 
            throw new NotImplementedException();
    }
}