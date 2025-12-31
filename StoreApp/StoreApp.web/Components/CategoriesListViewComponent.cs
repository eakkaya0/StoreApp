namespace StoreApp.web.Components;

using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.web.Models;

public class CategoriesListViewComponent : ViewComponent
{
    private readonly IStoreRepository _storeRepository;

    public CategoriesListViewComponent(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public IViewComponentResult Invoke()
    {

        ViewData["SelectedCategory"] = RouteData?.Values["category"];

        return View(_storeRepository.Categories
      .Select(c => new CategoryViewModel
      {
          Id = c.Id,
          Name = c.Name,
          Url = c.Url
      })
      .ToList());

    }
}