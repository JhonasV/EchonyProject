using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class secondStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Apellido = table.Column<string>(maxLength: 30, nullable: false),
                    Clave = table.Column<string>(maxLength: 30, nullable: false),
                    Codigo = table.Column<string>(nullable: true),
                    ConfirmacionClave = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    Mensaje = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(maxLength: 30, nullable: false),
                    Nombre = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    RutaFoto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fotos_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publiaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Contenido = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Foto = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publiaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publiaciones_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Contenido_comentario = table.Column<string>(nullable: true),
                    Fecha_Publicacion = table.Column<DateTime>(nullable: false),
                    Foto = table.Column<string>(nullable: true),
                    PublicacionesId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Publiaciones_PublicacionesId",
                        column: x => x.PublicacionesId,
                        principalTable: "Publiaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_PublicacionesId",
                table: "Comentarios",
                column: "PublicacionesId");

            migrationBuilder.CreateIndex(
                name: "IX_Publiaciones_UsuarioId",
                table: "Publiaciones",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "Publiaciones");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
