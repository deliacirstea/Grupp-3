using ShopAdmin.Commands;
using ShopAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAdmin.Tests.CommandsTests
{

    [TestClass]


    public class ProductTests
    {
        private readonly Product sut;

        public ProductTests()
        {
            sut = new Product();
        }

        [TestMethod]
        public void Is_Total_Amount_Of_Products_100_Then_True()
        {
            // ARRANGE
            var productList = new List<ProductModel>();
            for (int i = 0; i < 100; i++) 
            {
                productList.Add(new ProductModel
                {
                    ID = i,
                    Title = $"Product {i + 1}",
                    Description = $"This is product number {i + 1}",
                    Price = (i + 1) * 100_000,
                    Discount = i + 1,
                    Rating = new Random().Next(0, 6),
                    Stock = new Random().Next(10, 100),
                    Brand = "Totally Real Products Company",
                    Category = "Car Type",
                    Image = "string url"
                });
            }

            // ACT
            var result = productList.Count;

            //ASSERT
            Assert.AreEqual(result, 101);
        }
    }
}
