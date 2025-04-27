using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercadoD.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class Mig_20250427153513 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data2",
                table: "SaldosConsolidadosDiario",
                newName: "Data");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "SaldosConsolidadosDiario",
                newName: "Data2");
        }
    }
}
