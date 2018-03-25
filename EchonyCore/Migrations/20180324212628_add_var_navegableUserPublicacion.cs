using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class add_var_navegableUserPublicacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Publiaciones_PublicacionesId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Publiaciones_Usuario_UsuarioId",
                table: "Publiaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publiaciones",
                table: "Publiaciones");

            migrationBuilder.RenameTable(
                name: "Publiaciones",
                newName: "Publicaciones");

            migrationBuilder.RenameIndex(
                name: "IX_Publiaciones_UsuarioId",
                table: "Publicaciones",
                newName: "IX_Publicaciones_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publicaciones",
                table: "Publicaciones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Publicaciones_PublicacionesId",
                table: "Comentarios",
                column: "PublicacionesId",
                principalTable: "Publicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_Usuario_UsuarioId",
                table: "Publicaciones",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Publicaciones_PublicacionesId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_Usuario_UsuarioId",
                table: "Publicaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publicaciones",
                table: "Publicaciones");

            migrationBuilder.RenameTable(
                name: "Publicaciones",
                newName: "Publiaciones");

            migrationBuilder.RenameIndex(
                name: "IX_Publicaciones_UsuarioId",
                table: "Publiaciones",
                newName: "IX_Publiaciones_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publiaciones",
                table: "Publiaciones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Publiaciones_PublicacionesId",
                table: "Comentarios",
                column: "PublicacionesId",
                principalTable: "Publiaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publiaciones_Usuario_UsuarioId",
                table: "Publiaciones",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
