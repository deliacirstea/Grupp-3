using ShopAdmin.Models;

namespace ShopAdmin.Commands
{
    public class ExportProduct
    {
        public List<ProductModel> products { get; set; }

        public int total { get; set; }

        public int skip { get; set; } = 0;

        public int limit { get; set; } = 0;
    }

}