using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevagramCSharp.Migrations
{
    public partial class urlFotoPerfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlFotoPerfil",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlFotoPerfil",
                table: "Usuarios");
        }
    }
}
