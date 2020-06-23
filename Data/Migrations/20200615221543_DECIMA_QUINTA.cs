using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoS.Data.Migrations
{
    public partial class DECIMA_QUINTA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Devolucao");

            migrationBuilder.AddColumn<string>(
                name: "NomeCompleto",
                table: "Devolucao",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeCompleto",
                table: "Devolucao");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Devolucao",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
