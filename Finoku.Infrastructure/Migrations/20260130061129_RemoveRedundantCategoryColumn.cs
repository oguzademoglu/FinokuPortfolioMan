using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finoku.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRedundantCategoryColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Assets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
