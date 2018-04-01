using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EchonyCore.Migrations
{
    public partial class recepemi_final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<int>(
                name: "AmistadId",
                table: "Usuario",
                nullable: false,
                defaultValue: 0);

           

            

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Amistad",
                nullable: false,
                defaultValue: 0);

            

            

           

          

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            

            migrationBuilder.DropColumn(
                name: "AmistadId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Amistad");

            

            

            

            
           
        }
    }
}
