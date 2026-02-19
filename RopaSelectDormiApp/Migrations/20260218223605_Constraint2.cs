using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RopaSelectDormiApp.Migrations
{
    /// <inheritdoc />
    public partial class Constraint2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_clothes_list_elements_id_clothes_id_clothes_list",
                table: "clothes_list_elements");

            migrationBuilder.CreateIndex(
                name: "IX_clothes_list_elements_id_clothes_id_clothes_list",
                table: "clothes_list_elements",
                columns: new[] { "id_clothes", "id_clothes_list" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_clothes_list_elements_id_clothes_id_clothes_list",
                table: "clothes_list_elements");

            migrationBuilder.CreateIndex(
                name: "IX_clothes_list_elements_id_clothes_id_clothes_list",
                table: "clothes_list_elements",
                columns: new[] { "id_clothes", "id_clothes_list" });
        }
    }
}
