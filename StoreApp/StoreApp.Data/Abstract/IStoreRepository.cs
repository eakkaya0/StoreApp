using StoreApp.Data.Concrete;

namespace StoreApp.Data.Abstract
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products{ get; }

        IQueryable<Category> Categories{ get; }
        void CreateProduct(Product entity);

        int GetProductCount(string categoryUrl);

        IEnumerable<Product> GetProductsByCategory(string categoryUrl, int page, int pageSize);
    }
}