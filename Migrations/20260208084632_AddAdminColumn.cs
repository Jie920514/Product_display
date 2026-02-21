using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace 商品展示系統.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Permissions",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "Members");
        }
    }
}
