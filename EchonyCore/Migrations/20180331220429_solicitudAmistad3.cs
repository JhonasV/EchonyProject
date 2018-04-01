using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class solicitudAmistad3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudAmistad_Usuario_EmisorId",
                table: "SolicitudAmistad");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudAmistad_Usuario_ReceptorId",
                table: "SolicitudAmistad");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudAmistad_EmisorId",
                table: "SolicitudAmistad");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudAmistad_ReceptorId",
                table: "SolicitudAmistad");

            migrationBuilder.AlterColumn<int>(
                name: "ReceptorId",
                table: "SolicitudAmistad",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmisorId",
                table: "SolicitudAmistad",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmisorId1",
                table: "SolicitudAmistad",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceptorId1",
                table: "SolicitudAmistad",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "EmisorId1",
                table: "SolicitudAmistad");

            migrationBuilder.DropColumn(
                name: "ReceptorId1",
                table: "SolicitudAmistad");

            migrationBuilder.AlterColumn<int>(
                name: "ReceptorId",
                table: "SolicitudAmistad",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EmisorId",
                table: "SolicitudAmistad",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudAmistad_EmisorId",
                table: "SolicitudAmistad",
                column: "EmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudAmistad_ReceptorId",
                table: "SolicitudAmistad",
                column: "ReceptorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudAmistad_Usuario_EmisorId",
                table: "SolicitudAmistad",
                column: "EmisorId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudAmistad_Usuario_ReceptorId",
                table: "SolicitudAmistad",
                column: "ReceptorId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
