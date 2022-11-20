using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISeguros.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class CriadaTabelasProponenteSegurados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdadeMaximaSegurado",
                table: "Seguros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Proponentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proponentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Segurados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProponenteId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SobreNome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segurados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segurados_Proponentes_ProponenteId",
                        column: x => x.ProponenteId,
                        principalTable: "Proponentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dependentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeguradoId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependentes_Segurados_SeguradoId",
                        column: x => x.SeguradoId,
                        principalTable: "Segurados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dependentes_SeguradoId",
                table: "Dependentes",
                column: "SeguradoId");

            migrationBuilder.CreateIndex(
                name: "IX_Segurados_ProponenteId",
                table: "Segurados",
                column: "ProponenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependentes");

            migrationBuilder.DropTable(
                name: "Segurados");

            migrationBuilder.DropTable(
                name: "Proponentes");

            migrationBuilder.DropColumn(
                name: "IdadeMaximaSegurado",
                table: "Seguros");
        }
    }
}
