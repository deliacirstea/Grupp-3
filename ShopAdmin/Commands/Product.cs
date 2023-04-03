using Microsoft.Extensions.Logging;
using ShopAdmin.Models;
using ShopGeneral.Services;
using System.Net;
using System.Text.Json;

namespace ShopAdmin.Commands
{
    public class Product : ConsoleAppBase
    {
        private readonly ILogger<Product> _logger;
        private readonly IProductService _productService;

        public Product(ILogger<Product> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public void verifyimage()
        {
            _logger.LogInformation("VerifyImage starting");

            List<string> missingUrls = new();
            foreach (var product in _productService.GetAllProducts())
            {
                string imageUrl = product.ImageUrl;

                bool exists = DoesImageExist(imageUrl);
                if (!exists)
                {
                    Console.WriteLine($"Found a missing file:\n{imageUrl}");
                    missingUrls.Add(imageUrl);
                }
            }

            string directoryPath = "outfiles\\products\\";
            string filePath = $"{directoryPath}\\missingimages-{DateTime.UtcNow:yyyyMMdd}.txt";

            // Write missing urls to file
            if (missingUrls.Count > 0)
            {
                Directory.CreateDirectory(directoryPath); // Make sure all directories exists
                File.WriteAllLines(filePath, missingUrls);
            }

            _logger.LogInformation("VerifyImage ending");
        }
        public void export(string to)
        {
            _logger.LogInformation("Export starting");

            List<ProductModel> products = new();
            foreach (var currentProduct in _productService.GetAllProducts())
            {
                ProductModel product = new()
                {
                    ID = currentProduct.Id,
                    Title = currentProduct.Name,
                    Description = "",
                    Price = currentProduct.BasePrice,
                    Rating = 0,
                    Stock = 0,
                    Discount = 0,
                    Brand = currentProduct.ManufacturerName,
                    Category = currentProduct.CategoryName,
                    Image = currentProduct.ImageUrl,
                };
                products.Add(product);
            }

            ExportProduct result = new() { products = products, total = products.Count };

            string directoryPath = $"outfiles\\{to}\\";
            string filePath = $"{directoryPath}{DateTime.UtcNow:yyyyMMdd}.txt";

            // Serialize model to json string
            string json = JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true });

            // Output to file
            Directory.CreateDirectory(directoryPath); // Make sure all directories exists
            File.WriteAllText(filePath, json);

            _logger.LogInformation("Export ending");
        }

        private static bool DoesImageExist(string imageUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
            request.Method = "HEAD";

            bool exists;
            try
            {
                request.GetResponse();
                exists = true;
            }
            catch
            {
                exists = false;
            }
            return exists;
        }
    }
}
