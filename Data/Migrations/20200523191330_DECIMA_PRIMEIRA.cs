using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoS.Data.Migrations
{
    public partial class DECIMA_PRIMEIRA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocacaoSala");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilialId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdFilial",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdLocaSala",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdSala",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Setor",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelResponsavel",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FilialId",
                table: "AspNetUsers",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SalaId",
                table: "AspNetUsers",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Filial_FilialId",
                table: "AspNetUsers",
                column: "FilialId",
                principalTable: "Filial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sala_SalaId",
                table: "AspNetUsers",
                column: "SalaId",
                principalTable: "Sala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Filial_FilialId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sala_SalaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FilialId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SalaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FilialId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdFilial",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdLocaSala",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdSala",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Setor",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TelResponsavel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "LocacaoSala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: true),
                    IdFilial = table.Column<int>(type: "int", nullable: false),
                    IdSala = table.Column<int>(type: "int", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: true),
                    Setor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocacaoSala", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_LocacaoSala_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoSala_FilialId",
                table: "LocacaoSala",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoSala_SalaId",
                table: "LocacaoSala",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoSala_UserId",
                table: "LocacaoSala",
                column: "UserId");
        }
    }
}
