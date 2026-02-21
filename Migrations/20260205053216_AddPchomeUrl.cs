using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace 商品展示系統.Migrations
{
    /// <inheritdoc />
    public partial class AddPchomeUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PchomeUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PchomeUrl",
                table: "Products");
        }
    }
}
