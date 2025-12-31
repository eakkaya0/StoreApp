namespace StoreApp.Data.Concrete;

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;

public class EFStoreRepository : IStoreRepository
 {
     private readonly StoreDbContext _context;
 
     public EFStoreRepository(StoreDbContext context)
     {
         _context = context;
     }

    public IQueryable<Product> Products =>_context.Products;

    public IQueryable<Category> Categories =>_context.Categories;

    public void CreateProduct(Product entity)
    {
        throw new NotImplementedException();
    }

    public int GetProductCount(string categoryUrl)
    {
       return categoryUrl==null ? _context.Products.Count() :
            _context.Products.Where(p=>p.Categories.Any( c=>c.Url==categoryUrl)).Count();
    }

    public IEnumerable<Product> GetProductsByCategory(string categoryUrl, int page, int pageSize)
    {
        var products = Products;

        if(!string.IsNullOrEmpty(categoryUrl))
        {
            products=products.Where(p=>p.Categories.Any(c=>c.Url==categoryUrl));
        }

        return products.Skip((page-1)*pageSize).Take(pageSize);
    }
}