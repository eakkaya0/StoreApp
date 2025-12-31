namespace StoreApp.web.Components;

using Microsoft.AspNetCore.Mvc;
using StoreApp.web.Models;

public class CartSummaryViewComponent : ViewComponent
{
    private readonly Cart _cart;

    public CartSummaryViewComponent(Cart cart)
    {
        _cart = cart;
    }

    public IViewComponentResult Invoke()
    {
        return View(_cart);
    }
}