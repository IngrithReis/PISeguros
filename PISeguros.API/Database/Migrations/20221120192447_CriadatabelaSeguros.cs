﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISeguros.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class CriadatabelaSeguros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Seguros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VidasMinimas = table.Column<int>(type: "int", nullable: false),
                    MaxDependentes = table.Column<int>(type: "int", nullable: false),
                    ValorVida = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguros", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seguros");
        }
    }
}
