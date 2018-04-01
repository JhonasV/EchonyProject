using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class drop_amistad_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudAmistad_Usuario_EmisorId1",
                table: "SolicitudAmistad");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudAmistad_Usuario_ReceptorId1",
                table: "SolicitudAmistad");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudAmistad_EmisorId1",
                table: "SolicitudAmistad");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudAmistad_ReceptorId1",
                table: "SolicitudAmistad");

            migrationBuilder.DropColumn(
                name: "SolicitudAmistadId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "EmisorId1",
                table: "SolicitudAmistad");

            migrationBuilder.DropColumn(
                name: "ReceptorId1",
                table: "SolicitudAmistad");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "SolicitudAmistad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SolicitudAmistadId",
                table: "Usuario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmisorId1",
                table: "SolicitudAmistad",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceptorId1",
                table: "SolicitudAmistad",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "SolicitudAmistad",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudAmistad_EmisorId1",
                table: "SolicitudAmistad",
                column: "EmisorId1");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudAmistad_ReceptorId1",
                table: "SolicitudAmistad",
                column: "ReceptorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudAmistad_Usuario_EmisorId1",
                table: "SolicitudAmistad",
                column: "EmisorId1",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudAmistad_Usuario_ReceptorId1",
                table: "SolicitudAmistad",
                column: "ReceptorId1",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
