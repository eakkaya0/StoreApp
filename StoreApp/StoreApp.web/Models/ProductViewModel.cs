namespace StoreApp.web.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }
    public decimal Price { get; set; }
   
}

public class ProductListViewModel
{
    public IEnumerable<ProductViewModel> Products { get; set; }=Enumerable.Empty<ProductViewModel>();
    public PageInfo PageInfo { get; set; }=new ();
}

public class PageInfo
{
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalPages=> (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

    public int CurrentPage { get; set; }
   
}