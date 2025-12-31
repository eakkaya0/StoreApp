using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.web.Migrations
{
    /// <inheritdoc />
    public partial class CategoryAddUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Cotegories_CategoryId",
                table: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cotegories",
                table: "Cotegories");

            migrationBuilder.RenameTable(
                name: "Cotegories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Cotegories_Url",
                table: "Categories",
                newName: "IX_Categories_Url");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Categories_CategoryId",
                table: "ProductCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Categories_CategoryId",
                table: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Cotegories");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Url",
                table: "Cotegories",
                newName: "IX_Cotegories_Url");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cotegories",
                table: "Cotegories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Cotegories_CategoryId",
                table: "ProductCategory",
                column: "CategoryId",
                principalTable: "Cotegories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
