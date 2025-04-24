using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercadoD.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class Mig_20250424165449 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "SaldosConsolidadosDiario",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "ContasFinanceiras",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "SaldosConsolidadosDiario");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ContasFinanceiras");
        }
    }
}
