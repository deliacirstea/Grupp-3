using Microsoft.Extensions.Logging;
using ShopGeneral.Services;

namespace ShopAdmin.Commands
{
    public class Category : ConsoleAppBase
    {
        private readonly ILogger<Category> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public Category(ILogger<Category> logger, ICategoryService categoryService, IProductService productService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _productService = productService;
        }

        public void Checkempty()
        {
            _logger.LogInformation("CheckEmpty starting");

            string directoryPath = "outfiles\\category\\";
            string filePath = $"{directoryPath}missingproducts-{DateTime.UtcNow:yyyyMMdd}.txt";

            var categories = _categoryService.GetTrendingCategories(100); // TODO: Custom implementation
            var products = _productService.GetAllProducts(); // TODO: Custom implementation

            // Abstract away to an interface
            // Figure out tests:
            //   - Kategorier som har kopplade produkter bör inte loggas
            //   - Kategorier som inte har kopplade produkter bör loggas
            //   - Rätt kategori loggas

            // TODO: Declare the list here
            List<string> missingProducts = new();

            foreach (var category in categories)
            {
                bool isFound = false;
                foreach (var product in products)
                {
                    if (product.CategoryId == category.Id)
                    {
                        Console.WriteLine($"Category {category.Id} {category.Name} has at least one product linked.");
                        isFound = true;
                        break;
                    }
                }

                if(!isFound)
                {
                    Console.WriteLine($"Category {category.Id} {category.Name} has at least one product linked.");
                    // TODO: Add kategorins namn to the list of results
                    missingProducts.Add(category.Name);
                }
            }

            // TODO: Append the list of results to the file (only if listan.Count > 0)

            if (missingProducts.Count > 0)
            {
                Directory.CreateDirectory(directoryPath); // Make sure all directories exists
                File.AppendAllLines(filePath, missingProducts);
                _logger.LogInformation($"Logged{missingProducts.Count} missingproducts to {filePath}");
            }
            _logger.LogInformation("CheckEmpty ending");
        }
    }
}