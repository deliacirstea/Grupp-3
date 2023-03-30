using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using ShopGeneral.Services;

namespace ShopAdmin
{
    public class ProductModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }



        public ProductModel(int id, string title, string description,
            double price, double discount, double rating, int stock,
            string brand, string category, string image)
        {

            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Discount = discount;
            Rating = rating;
            Stock = stock;
            Brand = brand;
            Category = category;
            Image = image;

        }
        //







    }


}
