using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class amigos_usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "SolicitudAmistad",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudAmistad_UsuarioId",
                table: "SolicitudAmistad",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudAmistad_Usuario_UsuarioId",
                table: "SolicitudAmistad",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudAmistad_Usuario_UsuarioId",
                table: "SolicitudAmistad");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudAmistad_UsuarioId",
                table: "SolicitudAmistad");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "SolicitudAmistad");
        }
    }
}
