using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using StoreApp.web.Models;

public class OrderModel
{
    public int Id { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    [Required(ErrorMessage = "Ad Soyad alanı zorunludur")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Ad Soyad en az 3, en fazla 100 karakter olmalıdır")]
    public string CustomerName { get; set; } = null!;
    
    [Required(ErrorMessage = "Telefon numarası zorunludur")]
    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
    [RegularExpression(@"^0[0-9]{10}$", ErrorMessage = "Telefon numarası 0 ile başlamalı ve 11 haneli olmalıdır")]
    public string CustomerPhone { get; set; } = null!;
    
    [Required(ErrorMessage = "Şehir alanı zorunludur")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Şehir adı en az 2, en fazla 50 karakter olmalıdır")]
    public string CustomerCity { get; set; } = null!;
    
    [Required(ErrorMessage = "Adres alanı zorunludur")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Adres en az 10, en fazla 500 karakter olmalıdır")]
    public string CustomerAdressLine { get; set; } = null!;
    
    // Ödeme Bilgileri
    [Required(ErrorMessage = "Kart sahibinin adı zorunludur")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Kart sahibi adı en az 3, en fazla 100 karakter olmalıdır")]
    public string CardName { get; set; } = null!;
    
    [Required(ErrorMessage = "Kart numarası zorunludur")]
    [CreditCard(ErrorMessage = "Geçerli bir kart numarası giriniz")]
    [StringLength(19, MinimumLength = 13, ErrorMessage = "Geçerli bir kart numarası giriniz")]
    public string CardNumber { get; set; } = null!;
    
    [Required(ErrorMessage = "Son kullanma ayı zorunludur")]
    [RegularExpression(@"^(0[1-9]|1[0-2])$", ErrorMessage = "Geçerli bir ay giriniz (01-12)")]
    public string ExpirationMonth { get; set; } = null!;
    
    [Required(ErrorMessage = "Son kullanma yılı zorunludur")]
    [RegularExpression(@"^20[2-9][0-9]$", ErrorMessage = "Geçerli bir yıl giriniz")]
    public string ExpirationYear { get; set; } = null!;
    
    [Required(ErrorMessage = "CVV kodu zorunludur")]
    [RegularExpression(@"^[0-9]{3,4}$", ErrorMessage = "CVV 3 veya 4 haneli olmalıdır")]
    public string Cvc { get; set; } = null!;
    
    [BindNever]
    public Cart Cart { get; set; } = null!;
}