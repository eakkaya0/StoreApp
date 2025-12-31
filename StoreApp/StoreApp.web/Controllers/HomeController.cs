using Microsoft.AspNetCore.Mvc;
//using StoreApp.web.Models;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;

using AutoMapper;

namespace StoreApp.web.Controllers;

public class HomeController : Controller
{
    public int PageSize = 3;
    private readonly IStoreRepository _storeRepository;

    private readonly IMapper _autoMapper;

    public HomeController(IStoreRepository storeRepository, IMapper autoMapper)
    {
        _storeRepository = storeRepository;
        _autoMapper = autoMapper;
    }

    public IActionResult Index(string category, int page = 1) // categoryUrl yerine category
    {
        return View(new Models.ProductListViewModel
        {
            Products = _storeRepository.GetProductsByCategory(category, page, PageSize)
                .Select(p =>_autoMapper.Map<Models.ProductViewModel>(p)),      
            PageInfo = new Models.PageInfo
            {
                ItemsPerPage = PageSize,
                CurrentPage = page,
                TotalItems = _storeRepository.GetProductCount(category)
            }
        });
    }

}
