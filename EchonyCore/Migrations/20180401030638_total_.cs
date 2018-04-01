using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class total_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emisor");

            migrationBuilder.DropTable(
                name: "Receptor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emisor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SolicitudAmistadId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emisor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emisor_SolicitudAmistad_SolicitudAmistadId",
                        column: x => x.SolicitudAmistadId,
                        principalTable: "SolicitudAmistad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emisor_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receptor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SolicitudAmistadId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receptor_SolicitudAmistad_SolicitudAmistadId",
                        column: x => x.SolicitudAmistadId,
                        principalTable: "SolicitudAmistad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receptor_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emisor_SolicitudAmistadId",
                table: "Emisor",
                column: "SolicitudAmistadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emisor_UsuarioId",
                table: "Emisor",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptor_SolicitudAmistadId",
                table: "Receptor",
                column: "SolicitudAmistadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receptor_UsuarioId",
                table: "Receptor",
                column: "UsuarioId");
        }
    }
}
