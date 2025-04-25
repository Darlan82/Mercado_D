using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercadoD.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class Mig_20250424220411 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DtLancamento",
                table: "LancamentosFinanceiros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "SaldoPrevistoContabilizado",
                table: "LancamentosFinanceiros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SaldoRealizadoContabilizado",
                table: "LancamentosFinanceiros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoRealizado",
                table: "ContasFinanceiras",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtLancamento",
                table: "LancamentosFinanceiros");

            migrationBuilder.DropColumn(
                name: "SaldoPrevistoContabilizado",
                table: "LancamentosFinanceiros");

            migrationBuilder.DropColumn(
                name: "SaldoRealizadoContabilizado",
                table: "LancamentosFinanceiros");

            migrationBuilder.DropColumn(
                name: "SaldoRealizado",
                table: "ContasFinanceiras");
        }
    }
}
