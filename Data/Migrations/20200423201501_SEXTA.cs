using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoS.Data.Migrations
{
    public partial class SEXTA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocacaoEquipamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFilial = table.Column<int>(nullable: false),
                    FilialId = table.Column<int>(nullable: true),
                    IdSala = table.Column<int>(nullable: false),
                    SalaId = table.Column<int>(nullable: true),
                    IdEquipamento = table.Column<int>(nullable: false),
                    EquipamentoId = table.Column<int>(nullable: true),
                    LocEquiQtde = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    Responsável = table.Column<string>(nullable: true),
                    TelResponsavel = table.Column<string>(nullable: true),
                    Setor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocacaoEquipamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocacaoEquipamento_Equipamento_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocacaoEquipamento_Filial_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocacaoEquipamento_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocacaoSala",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFilial = table.Column<int>(nullable: false),
                    FilialId = table.Column<int>(nullable: true),
                    IdSala = table.Column<int>(nullable: false),
                    SalaId = table.Column<int>(nullable: true),
                    IdEquipamento = table.Column<int>(nullable: false),
                    EquipamentoId = table.Column<int>(nullable: true),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    Responsável = table.Column<string>(nullable: true),
                    TelResponsavel = table.Column<string>(nullable: true),
                    Setor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocacaoSala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocacaoSala_Equipamento_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocacaoSala_Filial_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocacaoSala_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoEquipamento_EquipamentoId",
                table: "LocacaoEquipamento",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoEquipamento_FilialId",
                table: "LocacaoEquipamento",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoEquipamento_SalaId",
                table: "LocacaoEquipamento",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoSala_EquipamentoId",
                table: "LocacaoSala",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoSala_FilialId",
                table: "LocacaoSala",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoSala_SalaId",
                table: "LocacaoSala",
                column: "SalaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocacaoEquipamento");

            migrationBuilder.DropTable(
                name: "LocacaoSala");
        }
    }
}
