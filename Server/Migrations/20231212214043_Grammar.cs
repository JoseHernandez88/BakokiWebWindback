using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakokiWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class Grammar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Cuentas_CuentaAccountNumber",
                table: "Transacciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transacciones",
                table: "Transacciones");

            migrationBuilder.RenameTable(
                name: "Transacciones",
                newName: "Transacciones");

            migrationBuilder.RenameIndex(
                name: "IX_Transacciones_CuentaAccountNumber",
                table: "Transacciones",
                newName: "IX_Transacciones_CuentaAccountNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacciones",
                table: "Transacciones",
                column: "TransactionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Cuentas_CuentaAccountNumber",
                table: "Transacciones",
                column: "CuentaAccountNumber",
                principalTable: "Cuentas",
                principalColumn: "AccountNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Cuentas_CuentaAccountNumber",
                table: "Transacciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transacciones",
                table: "Transacciones");

            migrationBuilder.RenameTable(
                name: "Transacciones",
                newName: "Transacciones");

            migrationBuilder.RenameIndex(
                name: "IX_Transacciones_CuentaAccountNumber",
                table: "Transacciones",
                newName: "IX_Transacciones_CuentaAccountNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacciones",
                table: "Transacciones",
                column: "TransactionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Cuentas_CuentaAccountNumber",
                table: "Transacciones",
                column: "CuentaAccountNumber",
                principalTable: "Cuentas",
                principalColumn: "AccountNumber");
        }
    }
}
