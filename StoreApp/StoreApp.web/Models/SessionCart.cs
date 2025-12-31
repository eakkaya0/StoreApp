namespace StoreApp.web.Models;

using StoreApp.Data.Concrete;
using StoreApp.web.Helpers;
using System.Text.Json.Serialization;


public class SessionCart : Cart
{
    public static SessionCart GetCart(IServiceProvider services)
    {
        var httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();
        var session = httpContextAccessor.HttpContext!.Session;
        var cart = session.GetObjectFromJson<SessionCart>("Cart") ?? new SessionCart();
        cart.Session = session;
        return cart;
    }
    [JsonIgnore]
    public ISession? Session { get; set; }
    override public void AddItem(Product product, int quantity)
    {
        base.AddItem(product, quantity);
        Session?.SetObjectAsJson("Cart", this);
    }
    override public void RemoveItem(Product product)
    {
        base.RemoveItem(product);
        Session?.SetObjectAsJson("Cart", this);
    }
    override public void Clear()
    {
        base.Clear();
        Session?.Remove("Cart");
    }
    override public void DecreaseItem(Product product)
    {
        base.DecreaseItem(product);
        Session?.SetObjectAsJson("Cart", this);
    }
}