using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class likes_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicacionnesId",
                table: "Like");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublicacionnesId",
                table: "Like",
                nullable: false,
                defaultValue: 0);
        }
    }
}
