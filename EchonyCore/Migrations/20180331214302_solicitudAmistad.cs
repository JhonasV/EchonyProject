using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class solicitudAmistad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amistad");

            migrationBuilder.RenameColumn(
                name: "AmistadId",
                table: "Usuario",
                newName: "SolicitudAmistadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SolicitudAmistadId",
                table: "Usuario",
                newName: "AmistadId");

            migrationBuilder.CreateTable(
                name: "Amistad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmisorId = table.Column<int>(nullable: true),
                    Estado = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    ReceptorId = table.Column<int>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amistad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amistad_Usuario_EmisorId",
                        column: x => x.EmisorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Amistad_Usuario_ReceptorId",
                        column: x => x.ReceptorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amistad_EmisorId",
                table: "Amistad",
                column: "EmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Amistad_ReceptorId",
                table: "Amistad",
                column: "ReceptorId");
        }
    }
}
