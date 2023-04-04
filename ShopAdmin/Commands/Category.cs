using Microsoft.Extensions.Logging;
using ShopGeneral.Services;

namespace ShopAdmin.Commands
{
    public class Category : ConsoleAppBase
    {
        private readonly ILogger<Product> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public Category(ILogger<Product> logger, ICategoryService categoryService, IProductService productService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _productService = productService;
        }

        public void checkempty()
        {
            _logger.LogInformation("CheckEmpty starting");

            string directoryPath = "outfiles\\category\\";
            string filePath = $"{directoryPath}\\missingproducts-{DateTime.UtcNow:yyyyMMdd}.txt";

            var categories = _categoryService.GetTrendingCategories(100);
            var products = _productService.GetAllProducts();

            // TODO: Declare the list here
            List<string> missingProducts = new();

            foreach (var category in categories)
            {
                bool isFound = false;
                foreach (var product in products)
                {
                    if (product.CategoryId == category.Id)
                    {
                        Console.WriteLine($"Kategori {category.Id} {category.Name} har minst en produkt kopplad.");
                        isFound = true;
                        break;
                    }
                }

                if(!isFound)
                {
                    Console.WriteLine($"Kategori {category.Id} {category.Name} har inga produkter kopplade.");
                    // TODO: Add kategorins namn to the list of results
                    missingProducts.Add(category.Name);
                }
            }

            // TODO: Append the list of results to the file (only if listan.Count > 0)

            if (missingProducts.Count > 0)
            {
                File.AppendAllLines(filePath, missingProducts);
                _logger.LogInformation($"Logged{missingProducts.Count} missingproducts to {filePath}");
            }
            _logger.LogInformation("CheckEmpty ending");
        }
    }
}