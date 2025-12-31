using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Data.Abstract;
using StoreApp.web.Helpers;
using StoreApp.web.Models;

namespace StoreApp.web.Pages
{
    public class CartModel : PageModel
    {
        private IStoreRepository _storeRepository;
        public CartModel(IStoreRepository storeRepository, Cart cartServices)
        {
            _storeRepository = storeRepository;
            Cart = cartServices;
        }
        public Cart Cart { get; set; } = new Cart();
        public void OnGet()
        {
         
        }

        public IActionResult OnPost(int Id)
        {
            var product = _storeRepository.Products.FirstOrDefault(p => p.Id == Id);
            if (product != null)
            {
               
                Cart.AddItem(product, 1);
               
            }
             return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult OnPostRemove(int id)
        {
            var product = _storeRepository.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                Cart.RemoveItem(product);
            }
            return RedirectToPage("/cart");
        }
        public IActionResult OnPostIncrease(int id)
        {
            // Session'dan cart'ı almayı UNUTMA
        

            var product = _storeRepository.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                Cart.AddItem(product, 1);
               
            }
            return RedirectToPage();
        }

        public IActionResult OnPostDecrease(int id)
        {
            // Session'dan cart'ı almayı UNUTMA
           

            var product = _storeRepository.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                Cart.DecreaseItem(product);
                
            }
            return RedirectToPage();
        }
        public IActionResult OnPostClear()
        {
            Cart.Clear();
            return RedirectToPage("/cart");
        }


    }
}
