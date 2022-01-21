using Microsoft.EntityFrameworkCore.Migrations;

namespace AppMVCBasica.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Completo",
                table: "Enderecos",
                newName: "Complemento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Complemento",
                table: "Enderecos",
                newName: "Completo");
        }
    }
}
