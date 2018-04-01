using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class prueba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emisor_Usuario_UsuarioId",
                table: "Emisor");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptor_Usuario_UsuarioId",
                table: "Receptor");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Receptor",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Emisor",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Emisor_Usuario_UsuarioId",
                table: "Emisor",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receptor_Usuario_UsuarioId",
                table: "Receptor",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emisor_Usuario_UsuarioId",
                table: "Emisor");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptor_Usuario_UsuarioId",
                table: "Receptor");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Receptor",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Emisor",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Emisor_Usuario_UsuarioId",
                table: "Emisor",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receptor_Usuario_UsuarioId",
                table: "Receptor",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
