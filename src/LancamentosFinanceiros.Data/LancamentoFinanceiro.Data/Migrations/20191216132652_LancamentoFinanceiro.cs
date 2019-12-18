using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LancamentoFinanceiro.Data.Migrations
{
    public partial class LancamentoFinanceiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LancamentoFinanceiros",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataLancamento = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancamentoFinanceiros", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LancamentoFinanceiros");
        }
    }
}
