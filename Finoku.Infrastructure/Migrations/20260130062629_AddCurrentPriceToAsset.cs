using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finoku.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentPriceToAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                table: "Assets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "Assets");
        }
    }
}
