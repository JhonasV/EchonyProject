using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class detalles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DetallesId",
                table: "Usuario",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Detalles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    Fecha_Nacimiento = table.Column<DateTime>(nullable: false),
                    Pais = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_DetallesId",
                table: "Usuario",
                column: "DetallesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Detalles_DetallesId",
                table: "Usuario",
                column: "DetallesId",
                principalTable: "Detalles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Detalles_DetallesId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Detalles");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_DetallesId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "DetallesId",
                table: "Usuario");
        }
    }
}
