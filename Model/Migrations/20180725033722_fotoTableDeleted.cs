using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class fotoTableDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Usuario",
                nullable: true);

           /* migrationBuilder.CreateIndex(
                name: "IX_CommentReply_UsuarioId",
                table: "CommentReply",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReply_Usuario_UsuarioId",
                table: "CommentReply",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReply_Usuario_UsuarioId",
                table: "CommentReply");

            migrationBuilder.DropIndex(
                name: "IX_CommentReply_UsuarioId",
                table: "CommentReply");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Usuario");

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
        }
    }
}
