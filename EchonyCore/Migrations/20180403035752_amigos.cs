using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class amigos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amigos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmisorId = table.Column<int>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    ReceptorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amigos_Emisor_EmisorId",
                        column: x => x.EmisorId,
                        principalTable: "Emisor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Amigos_Receptor_ReceptorId",
                        column: x => x.ReceptorId,
                        principalTable: "Receptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_EmisorId",
                table: "Amigos",
                column: "EmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_ReceptorId",
                table: "Amigos",
                column: "ReceptorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amigos");
        }
    }
}
