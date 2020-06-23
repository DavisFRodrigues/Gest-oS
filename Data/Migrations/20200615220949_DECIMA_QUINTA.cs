using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoS.Data.Migrations
{
    public partial class DECIMA_QUINTA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devolucao_Equipamento_EquipamentoId",
                table: "Devolucao");

            migrationBuilder.DropForeignKey(
                name: "FK_Devolucao_LocacaoEquipamento_LocaEquipamentoId",
                table: "Devolucao");

            migrationBuilder.DropForeignKey(
                name: "FK_Devolucao_AspNetUsers_UserId",
                table: "Devolucao");

            migrationBuilder.DropIndex(
                name: "IX_Devolucao_EquipamentoId",
                table: "Devolucao");

            migrationBuilder.DropIndex(
                name: "IX_Devolucao_LocaEquipamentoId",
                table: "Devolucao");

            migrationBuilder.DropIndex(
                name: "IX_Devolucao_UserId",
                table: "Devolucao");

            migrationBuilder.DropColumn(
                name: "EquipamentoId",
                table: "Devolucao");

            migrationBuilder.DropColumn(
                name: "IdEquipamento",
                table: "Devolucao");

            migrationBuilder.DropColumn(
                name: "LocaEquipamentoId",
                table: "Devolucao");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Devolucao",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NmPatrimonio",
                table: "Devolucao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NomeEquipamento",
                table: "Devolucao",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NmPatrimonio",
                table: "Devolucao");

            migrationBuilder.DropColumn(
                name: "NomeEquipamento",
                table: "Devolucao");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Devolucao",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipamentoId",
                table: "Devolucao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEquipamento",
                table: "Devolucao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocaEquipamentoId",
                table: "Devolucao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devolucao_EquipamentoId",
                table: "Devolucao",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Devolucao_LocaEquipamentoId",
                table: "Devolucao",
                column: "LocaEquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Devolucao_UserId",
                table: "Devolucao",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devolucao_Equipamento_EquipamentoId",
                table: "Devolucao",
                column: "EquipamentoId",
                principalTable: "Equipamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devolucao_LocacaoEquipamento_LocaEquipamentoId",
                table: "Devolucao",
                column: "LocaEquipamentoId",
                principalTable: "LocacaoEquipamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devolucao_AspNetUsers_UserId",
                table: "Devolucao",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
