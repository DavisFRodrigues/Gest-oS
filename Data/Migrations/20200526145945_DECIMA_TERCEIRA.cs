using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoS.Data.Migrations
{
    public partial class DECIMA_TERCEIRA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responsável",
                table: "LocacaoEquipamento");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LocacaoEquipamento",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoEquipamento_UserId",
                table: "LocacaoEquipamento",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocacaoEquipamento_AspNetUsers_UserId",
                table: "LocacaoEquipamento",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocacaoEquipamento_AspNetUsers_UserId",
                table: "LocacaoEquipamento");

            migrationBuilder.DropIndex(
                name: "IX_LocacaoEquipamento_UserId",
                table: "LocacaoEquipamento");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LocacaoEquipamento");

            migrationBuilder.AddColumn<string>(
                name: "Responsável",
                table: "LocacaoEquipamento",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
