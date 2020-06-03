using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoS.Data.Migrations
{
    public partial class Oitava : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responsável",
                table: "LocacaoSala");

            migrationBuilder.AddColumn<string>(
                name: "Responsavel",
                table: "LocacaoSala",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoSala_Responsavel",
                table: "LocacaoSala",
                column: "Responsavel");

            migrationBuilder.AddForeignKey(
                name: "FK_LocacaoSala_AspNetUsers_Responsavel",
                table: "LocacaoSala",
                column: "Responsavel",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocacaoSala_AspNetUsers_Responsavel",
                table: "LocacaoSala");

            migrationBuilder.DropIndex(
                name: "IX_LocacaoSala_Responsavel",
                table: "LocacaoSala");

            migrationBuilder.DropColumn(
                name: "Responsavel",
                table: "LocacaoSala");

            migrationBuilder.AddColumn<string>(
                name: "Responsável",
                table: "LocacaoSala",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
