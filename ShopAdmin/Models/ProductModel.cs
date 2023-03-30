using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAdmin.Models
{
    public class ProductModel
    {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
            public double Discount { get; set; }
            public double Rating { get; set; }
            public int Stock { get; set; }
            public string Brand { get; set; }
            public string Category { get; set; }
            public string Image { get; set; }
        }
   }
