using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercadoD.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class Mig_20250427153411 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "SaldosConsolidadosDiario");

            migrationBuilder.AddColumn<int>(
                name: "Data2",
                table: "SaldosConsolidadosDiario",
                type: "int",
                nullable: false
                );

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "LancamentosFinanceiros",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Conta_Data",
                table: "SaldosConsolidadosDiario",
                columns: new[] { "ContaId", "Data2" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Conta_Data",
                table: "SaldosConsolidadosDiario");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "LancamentosFinanceiros");

            migrationBuilder.DropColumn(
                name: "Data2",
                table: "SaldosConsolidadosDiario");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "SaldosConsolidadosDiario",
                type: "datetime2",
                nullable: false);
        }
    }
}
