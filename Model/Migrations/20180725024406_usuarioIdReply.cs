using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class usuarioIdReply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReply_Usuario_UsuarioId",
                table: "CommentReply");

            migrationBuilder.DropIndex(
                name: "IX_CommentReply_UsuarioId",
                table: "CommentReply");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "CommentReply");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "CommentReply",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentReply_UsuarioId",
                table: "CommentReply",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReply_Usuario_UsuarioId",
                table: "CommentReply",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
