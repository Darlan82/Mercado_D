using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercadoD.Infra.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DtCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContasFinanceiras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SaldoPrevisto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    LojaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContasFinanceiras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContasFinanceiras_Lojas_LojaId",
                        column: x => x.LojaId,
                        principalTable: "Lojas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LancamentosFinanceiros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DtVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancamentosFinanceiros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LancamentosFinanceiros_ContasFinanceiras_ContaId",
                        column: x => x.ContaId,
                        principalTable: "ContasFinanceiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaldosConsolidadosDiario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaldoPrevisto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoRealizado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaldosConsolidadosDiario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaldosConsolidadosDiario_ContasFinanceiras_ContaId",
                        column: x => x.ContaId,
                        principalTable: "ContasFinanceiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContasFinanceiras_LojaId",
                table: "ContasFinanceiras",
                column: "LojaId");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosFinanceiros_ContaId",
                table: "LancamentosFinanceiros",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_SaldosConsolidadosDiario_ContaId",
                table: "SaldosConsolidadosDiario",
                column: "ContaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LancamentosFinanceiros");

            migrationBuilder.DropTable(
                name: "SaldosConsolidadosDiario");

            migrationBuilder.DropTable(
                name: "ContasFinanceiras");

            migrationBuilder.DropTable(
                name: "Lojas");
        }
    }
}
