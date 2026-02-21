using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace 商品展示系統.Migrations
{
    /// <inheritdoc />
    public partial class RenameKeyToProdId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specification",
                table: "Products",
                newName: "Prod_Specification");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "Prod_Price");

            migrationBuilder.RenameColumn(
                name: "PchomeUrl",
                table: "Products",
                newName: "Prod_Url");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "Prod_Name");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "Prod_Image_Url");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Products",
                newName: "Prod_Brand");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "Prod_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prod_Url",
                table: "Products",
                newName: "PchomeUrl");

            migrationBuilder.RenameColumn(
                name: "Prod_Specification",
                table: "Products",
                newName: "Specification");

            migrationBuilder.RenameColumn(
                name: "Prod_Price",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Prod_Name",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Prod_Image_Url",
                table: "Products",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Prod_Brand",
                table: "Products",
                newName: "Brand");

            migrationBuilder.RenameColumn(
                name: "Prod_Id",
                table: "Products",
                newName: "Id");
        }
    }
}
