using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoS.Data.Migrations
{
    public partial class SETIMA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocacaoSala_Equipamento_EquipamentoId",
                table: "LocacaoSala");

            migrationBuilder.DropIndex(
                name: "IX_LocacaoSala_EquipamentoId",
                table: "LocacaoSala");

            migrationBuilder.DropColumn(
                name: "EquipamentoId",
                table: "LocacaoSala");

            migrationBuilder.DropColumn(
                name: "IdEquipamento",
                table: "LocacaoSala");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipamentoId",
                table: "LocacaoSala",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEquipamento",
                table: "LocacaoSala",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoSala_EquipamentoId",
                table: "LocacaoSala",
                column: "EquipamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocacaoSala_Equipamento_EquipamentoId",
                table: "LocacaoSala",
                column: "EquipamentoId",
                principalTable: "Equipamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
