using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.web.Models;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(typeof(StoreApp.web.Models.MapperProfile).Assembly);  

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreAppConnection"),b => b.MigrationsAssembly("StoreApp.web")));

builder.Services.AddScoped<IStoreRepository,EFStoreRepository>(); 
builder.Services.AddScoped<IOrderRepository,EFOrderRepository>();  
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute("prodcuts_in_category","products/{category?}",new {controller="Home",action="Index"}) ; 
app.MapControllerRoute("prodcuts_details","{name}",new {controller="Home",action="Details"});
   
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
