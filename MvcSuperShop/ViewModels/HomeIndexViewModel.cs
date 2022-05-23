namespace MvcSuperShop.ViewModels;

public class HomeIndexViewModel
{
    public List<CategoryViewModel> TrendingCategories { get; set; }
    public List<ProductBoxViewModel> NewProducts { get; set; }
}

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
}

public class ProductBoxViewModel
{
    public int Id { get; set; }
    public int Price { get; set; }
    public string Name { get; set; }
    public string ManufacturerName { get; set; }
    public string CategoryName { get; set; }
    public string ImageUrl { get; set; }
}