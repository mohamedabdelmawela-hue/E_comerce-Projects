using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myStore.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_categoryModelcategoryId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_categoryModelcategoryId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "categoryModelcategoryId",
                table: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoryModelcategoryId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_categoryModelcategoryId",
                table: "Category",
                column: "categoryModelcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_categoryModelcategoryId",
                table: "Category",
                column: "categoryModelcategoryId",
                principalTable: "Category",
                principalColumn: "categoryId");
        }
    }
}
