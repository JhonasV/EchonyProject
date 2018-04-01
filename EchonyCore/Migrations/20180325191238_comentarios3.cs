using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class comentarios3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_PublicacionesId",
                table: "Comentarios",
                column: "PublicacionesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Publicaciones_PublicacionesId",
                table: "Comentarios",
                column: "PublicacionesId",
                principalTable: "Publicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Publicaciones_PublicacionesId",
                table: "Comentarios");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_PublicacionesId",
                table: "Comentarios");
        }
    }
}
