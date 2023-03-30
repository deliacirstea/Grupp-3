using Microsoft.Extensions.Logging;
using ShopGeneral.Data;
using System.Net;

namespace ShopAdmin.Commands
{
    public class Product : ConsoleAppBase
    {
        private readonly ILogger<Product> _logger;
        private readonly ApplicationDbContext _dbContext;

        public Product(ILogger<Product> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void verifyimage()
        {
            _logger.LogInformation("VerifyImage starting");

            List<string> missingUrls = new();
            foreach (var product in _dbContext.Products)
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
