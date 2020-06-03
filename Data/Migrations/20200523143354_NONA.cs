using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoS.Data.Migrations
{
    public partial class NONA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocacaoSala_AspNetUsers_Responsavel",
                table: "LocacaoSala");

            migrationBuilder.RenameColumn(
                name: "Responsavel",
                table: "LocacaoSala",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LocacaoSala_Responsavel",
                table: "LocacaoSala",
                newName: "IX_LocacaoSala_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocacaoSala_AspNetUsers_UserId",
                table: "LocacaoSala",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocacaoSala_AspNetUsers_UserId",
                table: "LocacaoSala");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LocacaoSala",
                newName: "Responsavel");

            migrationBuilder.RenameIndex(
                name: "IX_LocacaoSala_UserId",
                table: "LocacaoSala",
                newName: "IX_LocacaoSala_Responsavel");

            migrationBuilder.AddForeignKey(
                name: "FK_LocacaoSala_AspNetUsers_Responsavel",
                table: "LocacaoSala",
                column: "Responsavel",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
