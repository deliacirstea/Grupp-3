using ShopAdmin.Models;

namespace ShopAdmin.Commands
{
    public class ExportProduct
    {
        public List<ProductModel> Products { get; set; }

        public int Total { get; set; }

        public int Skip { get; set; } = 0;

        public int Limit { get; set; } = 0;
    }

}