using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class em_re : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmisorId",
                table: "SolicitudAmistad");

            migrationBuilder.DropColumn(
                name: "ReceptorId",
                table: "SolicitudAmistad");

            migrationBuilder.CreateTable(
                name: "Emisor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    SolicitudAmistadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emisor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emisor_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emisor_SolicitudAmistad_SolicitudAmistadId",
                        column: x => x.SolicitudAmistadId,
                        principalTable: "SolicitudAmistad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receptor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    SolicitudAmistadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receptor_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receptor_SolicitudAmistad_SolicitudAmistadId",
                        column: x => x.SolicitudAmistadId,
                        principalTable: "SolicitudAmistad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emisor_SolicitudAmistadId",
                table: "Emisor",
                column: "SolicitudAmistadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receptor_SolicitudAmistadId",
                table: "Receptor",
                column: "SolicitudAmistadId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emisor");

            migrationBuilder.DropTable(
                name: "Receptor");

            migrationBuilder.AddColumn<int>(
                name: "EmisorId",
                table: "SolicitudAmistad",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReceptorId",
                table: "SolicitudAmistad",
                nullable: false,
                defaultValue: 0);
        }
    }
}
