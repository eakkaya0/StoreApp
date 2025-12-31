using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApp.web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Elektronik", "13 inç, Intel i7, 16GB RAM, 512GB SSD", "Dell XPS 13 9310", 58000m },
                    { 2, "Elektronik", "128GB, A17 Bionic, OLED Ekran", "iPhone 15 Pro", 80000m },
                    { 3, "Aksesuar", "Gürültü önleyici kablosuz kulaklık", "Sony WH-1000XM5", 9000m },
                    { 4, "Elektronik", "11 inç, 256GB, Android Tablet", "Samsung Galaxy Tab S9", 45000m },
                    { 5, "Aksesuar", "Kablosuz mouse, ergonomik tasarım", "Logitech MX Master 3", 2500m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
