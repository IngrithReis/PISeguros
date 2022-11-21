using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISeguros.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class IncluidoCampoSeguroId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeguroId",
                table: "Proponentes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeguroId",
                table: "Proponentes");
        }
    }
}
