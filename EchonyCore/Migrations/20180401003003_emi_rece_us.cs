using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class emi_rece_us : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emisor_Usuario_Id",
                table: "Emisor");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptor_Usuario_Id",
                table: "Receptor");

          
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Receptor",
                nullable: false,
                defaultValue: 0);

          

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Emisor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receptor_UsuarioId",
                table: "Receptor",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Emisor_UsuarioId",
                table: "Emisor",
                column: "UsuarioId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emisor_Usuario_UsuarioId",
                table: "Emisor");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptor_Usuario_UsuarioId",
                table: "Receptor");

            migrationBuilder.DropIndex(
                name: "IX_Receptor_UsuarioId",
                table: "Receptor");

            migrationBuilder.DropIndex(
                name: "IX_Emisor_UsuarioId",
                table: "Emisor");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Receptor");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Emisor");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Receptor",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Emisor",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Emisor_Usuario_Id",
                table: "Emisor",
                column: "Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receptor_Usuario_Id",
                table: "Receptor",
                column: "Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
