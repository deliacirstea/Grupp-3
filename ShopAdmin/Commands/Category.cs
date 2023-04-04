using Microsoft.Extensions.Logging;
using ShopAdmin.Models;
using ShopGeneral.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAdmin.Commands
{
    public class Category
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



            public void checkempty()
            {
                _logger.LogInformation("Checking category starting");

                string directoryPath = "outfiles\\categories\\";
                string filePath = $"{directoryPath}\\missingproducts-{DateTime.UtcNow:yyyyMMdd}.txt";

                var categories = _productService.GetAllProducts();

                foreach (var category in categories)
                {
                    var products = _productService.GetProductsByCategoryId(category.Id);

                    
                    if (products.Count() == 0)
                    {
                        string categoryName = category.Name;
                        _logger.LogInformation($"Category {categoryName} has no products");
                        System.IO.File.AppendAllText(filePath, categoryName + Environment.NewLine);
                    }
                }

                _logger.LogInformation("Checking category ending");
            }




            //public void checkempty()
            //{

            //    _logger.LogInformation("Checking category starting");

            //    List<ProductModel> products = new();

            //    foreach (var product in _productService.GetAllProducts())
            //    {

            //        int category = product.CategoryId;





            //    }


            //    string directoryPath = "outfiles\\categories\\";
            //    string filePath = $"{directoryPath}\\missingcategories-{DateTime.UtcNow:yyyyMMdd}.txt";

            //    _logger.LogInformation("Checking category ending");

            //}







        }
    }
}