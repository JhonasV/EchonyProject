using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class usuarioIdReply2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "CommentReply",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "CommentReply");
        }
    }
}
