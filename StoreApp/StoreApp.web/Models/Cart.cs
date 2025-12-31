using System;
using StoreApp.Data.Concrete;

namespace StoreApp.web.Models;

public class Cart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public virtual void AddItem(Product product, int quantity)
    {
        var existingItem = Items.FirstOrDefault(i => i.Product.Id == product.Id);
        if (existingItem == null)
        {
            Items.Add(new CartItem
            {
                Product = product,
                Quantity = quantity
            });
        }
        else
        {
            existingItem.Quantity += quantity;
        }
    }
    public virtual void RemoveItem(Product product)
    {
        Items.RemoveAll(i => i.Product.Id == product.Id);
    }
    public decimal ComputeTotalValue()
    {
        return Items.Sum(i =>(decimal) i.Product.Price * i.Quantity);
    }
    public virtual void Clear()
    {
        Items.Clear();
    }
    public virtual void DecreaseItem(Product product)
    {
        var item = Items.FirstOrDefault(i => i.Product.Id == product.Id);
        if (item != null)
        {
            item.Quantity--;

            if (item.Quantity <= 0)
                Items.Remove(item);
        }
    }


}

public class CartItem
{
    public int CartItemId { get; set; }

    public Product Product { get; set; } = new Product();
    public int Quantity { get; set; }
}