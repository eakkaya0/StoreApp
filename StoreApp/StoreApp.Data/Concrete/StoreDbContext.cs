namespace StoreApp.Data.Concrete;

using Microsoft.EntityFrameworkCore;



public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Order> Orders{get; set;} 


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       modelBuilder.Entity<Product>()
        .HasMany(e => e.Categories)
        .WithMany(e => e.Products)
        .UsingEntity<ProductCategory>();

        modelBuilder.Entity<Category>()
        .HasIndex(c => c.Url).IsUnique();
           

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Dell XPS 13 9310", Description = "13 inç, Intel i7, 16GB RAM, 512GB SSD", Price = 58000 },
            new Product { Id = 2, Name = "iPhone 15 Pro", Description = "128GB, A17 Bionic, OLED Ekran", Price = 80000  },
            new Product { Id = 3, Name = "Sony WH-1000XM5", Description = "Gürültü önleyici kablosuz kulaklık", Price = 9000 },
            new Product { Id = 4, Name = "Samsung Galaxy Tab S9", Description = "11 inç, 256GB, Android Tablet", Price = 45000  },
            new Product { Id = 5, Name = "Logitech MX Master 3", Description = "Kablosuz mouse, ergonomik tasarım", Price = 2500 }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Elektronik", Url = "elektronik" },
            new Category { Id = 2, Name = "Bilgisayar", Url = "bilgisayar" },
            new Category { Id = 3, Name = "Telefon", Url = "telefon" },
            new Category { Id = 4, Name = "Aksesuar", Url = "aksesuar" },
            new Category { Id = 5, Name = "Beyaz Eşya", Url = "beyaz-esya" }
        );

        modelBuilder.Entity<ProductCategory>().HasData(
            new ProductCategory { ProductId = 1, CategoryId = 1 },
            new ProductCategory { ProductId = 1, CategoryId = 2 },
            new ProductCategory { ProductId = 2, CategoryId = 1 },
            new ProductCategory { ProductId = 2, CategoryId = 3 },
            new ProductCategory { ProductId = 3, CategoryId = 1 },
            new ProductCategory { ProductId = 3, CategoryId = 4 },
            new ProductCategory { ProductId = 4, CategoryId = 1 },
            new ProductCategory { ProductId = 4, CategoryId = 3 },
            new ProductCategory { ProductId = 5, CategoryId = 4 }
        );
    }


}