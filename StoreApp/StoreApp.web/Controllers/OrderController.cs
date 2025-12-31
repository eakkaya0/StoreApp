namespace StoreApp.web.Controllers;

using AspNetCoreGeneratedDocument;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.web.Models;

public class OrderController : Controller
{
    private Cart _cart;
    private IOrderRepository _orderRepository;

    public OrderController(Cart cart, IOrderRepository orderRepository)
    {
        _cart = cart;
        _orderRepository = orderRepository;
    }

    // GET - Formu göster
    [HttpGet]
    public IActionResult CheckOut()
    {
        return View(new OrderModel() { Cart = _cart });
    }

    // POST - Form gönderildiğinde çalışır
    [HttpPost]
    public async Task<IActionResult> CheckOut(OrderModel model)
    {
        // ✅ ÖNCE Cart'ı yükle (model.Cart null gelir çünkü [BindNever])
        model.Cart = _cart;

        // Sepet boş mu kontrol et
        if (model.Cart.Items.Count == 0)
        {
            ModelState.AddModelError("", "Sepetinizde ürün yok");
        }

        // Cart alanını validasyondan çıkar (yoksa "Cart is required" hatası verir)
        ModelState.Remove("Cart");

        // Validasyon kontrolü
        if (ModelState.IsValid)
        {
            // Ödeme işlemini başlat
            var payment = await ProcessPayment(model);

            if (payment.Status == "success")
            {
                // ✅ Ödeme başarılıysa siparişi kaydet
                var order = new Order
                {
                    CustomerName = model.CustomerName,
                    CustomerPhone = model.CustomerPhone,
                    CustomerCity = model.CustomerCity,
                    CustomerAdressLine = model.CustomerAdressLine,
                    OrderDate = DateTime.Now,
                    OrderItems = _cart.Items.Select(i => new StoreApp.Data.Concrete.OrderItem
                    {
                        ProductId = i.Product.Id,
                        UnitPrice =(decimal) i.Product.Price,
                        Quantity = i.Quantity,
                    }).ToList()
                };

                _orderRepository.SaveOrder(order);

                // Sepeti temizle
                _cart.Clear();

                return RedirectToPage("/Completed", new { Orderid = order.Id });
            }
            else
            {
                // ❌ Ödeme başarısızsa hata mesajı göster
                ModelState.AddModelError("", $"Ödeme işlemi başarısız: {payment.ErrorMessage}");
                return View(model);
            }
        }
        else
        {
            // Hata varsa model.Cart zaten yukarda yüklenmiş
            return View(model);
        }
    }

    private async Task<Payment> ProcessPayment(OrderModel model)
    {
        Options options = new Options();
        options.ApiKey = "sandbox-d9dZPr2vcWdO8DENXAI6jKm6uokkPmld";
        options.SecretKey = "sandbox-4sKouYGCB4dP1r4AcLy3ephX4dOez1hH";
        options.BaseUrl = "https://sandbox-api.iyzipay.com";

        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = Guid.NewGuid().ToString(); // ✅ Her işlem için benzersiz ID
        
        // ✅ Sepetteki toplam tutarı hesapla
        decimal totalPrice = (decimal)model.Cart.ComputeTotalValue();
        request.Price = totalPrice.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
        request.PaidPrice = totalPrice.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
        
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = Guid.NewGuid().ToString().Substring(0, 8); // ✅ Benzersiz sepet ID
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        // ✅ Formdan gelen kart bilgilerini kullan
        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = model.CardName;
        paymentCard.CardNumber = model.CardNumber.Replace(" ", ""); // Boşlukları temizle
        paymentCard.ExpireMonth = model.ExpirationMonth;
        paymentCard.ExpireYear = model.ExpirationYear;
        paymentCard.Cvc = model.Cvc;
        paymentCard.RegisterCard = 0;
        request.PaymentCard = paymentCard;

        // ✅ Müşteri bilgilerini formdan al
        Buyer buyer = new Buyer();
        buyer.Id = Guid.NewGuid().ToString().Substring(0, 8);
        
        // Ad ve soyadı ayır
        string[] nameParts = model.CustomerName.Split(' ', 2);
        buyer.Name = nameParts[0];
        buyer.Surname = nameParts.Length > 1 ? nameParts[1] : "";
        
        buyer.GsmNumber = model.CustomerPhone;
        buyer.Email = "musteri@email.com"; // ✅ İsterseniz forma email alanı ekleyebilirsiniz
        buyer.IdentityNumber = "11111111111"; // ✅ İsterseniz forma TC kimlik alanı ekleyebilirsiniz
        buyer.LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        buyer.RegistrationDate = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss");
        buyer.RegistrationAddress = model.CustomerAdressLine;
        buyer.Ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
        buyer.City = model.CustomerCity;
        buyer.Country = "Turkey";
        buyer.ZipCode = "34000";
        request.Buyer = buyer;

        // ✅ Teslimat adresi
        Address shippingAddress = new Address();
        shippingAddress.ContactName = model.CustomerName;
        shippingAddress.City = model.CustomerCity;
        shippingAddress.Country = "Turkey";
        shippingAddress.Description = model.CustomerAdressLine;
        shippingAddress.ZipCode = "34000";
        request.ShippingAddress = shippingAddress;

        // ✅ Fatura adresi (teslimat ile aynı)
        Address billingAddress = new Address();
        billingAddress.ContactName = model.CustomerName;
        billingAddress.City = model.CustomerCity;
        billingAddress.Country = "Turkey";
        billingAddress.Description = model.CustomerAdressLine;
        billingAddress.ZipCode = "34000";
        request.BillingAddress = billingAddress;

        // ✅ Sepetteki ürünleri BasketItem'a dönüştür
        List<BasketItem> basketItems = new List<BasketItem>();
        foreach (var item in model.Cart.Items)
        {
            BasketItem basketItem = new BasketItem();
            basketItem.Id = item.Product.Id.ToString();
            basketItem.Name = item.Product.Name ?? "Ürün";
            basketItem.Category1 = "Ürünler";
            basketItem.Category2 = "Genel";
            basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            basketItem.Price = (item.Product.Price * item.Quantity).ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
            basketItems.Add(basketItem);
        }
        request.BasketItems = basketItems;

        // ✅ Ödeme işlemini gerçekleştir
        Payment payment = await Payment.Create(request, options);

        return payment;
    }
}