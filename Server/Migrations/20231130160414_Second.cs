using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakokiWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuenta_Clientes_ClienteEmail",
                table: "Cuenta");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacion_Cuenta_CuentaAccountNumber",
                table: "Transacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transacion",
                table: "Transacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuenta",
                table: "Cuenta");

            migrationBuilder.RenameTable(
                name: "Transacion",
                newName: "Transaciones");

            migrationBuilder.RenameTable(
                name: "Cuenta",
                newName: "Cuentas");

            migrationBuilder.RenameIndex(
                name: "IX_Transacion_CuentaAccountNumber",
                table: "Transaciones",
                newName: "IX_Transaciones_CuentaAccountNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Cuenta_ClienteEmail",
                table: "Cuentas",
                newName: "IX_Cuentas_ClienteEmail");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaciones",
                table: "Transaciones",
                column: "TransactionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuentas",
                table: "Cuentas",
                column: "AccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuentas_Clientes_ClienteEmail",
                table: "Cuentas",
                column: "ClienteEmail",
                principalTable: "Clientes",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaciones_Cuentas_CuentaAccountNumber",
                table: "Transaciones",
                column: "CuentaAccountNumber",
                principalTable: "Cuentas",
                principalColumn: "AccountNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuentas_Clientes_ClienteEmail",
                table: "Cuentas");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaciones_Cuentas_CuentaAccountNumber",
                table: "Transaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaciones",
                table: "Transaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuentas",
                table: "Cuentas");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "Transaciones",
                newName: "Transacion");

            migrationBuilder.RenameTable(
                name: "Cuentas",
                newName: "Cuenta");

            migrationBuilder.RenameIndex(
                name: "IX_Transaciones_CuentaAccountNumber",
                table: "Transacion",
                newName: "IX_Transacion_CuentaAccountNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Cuentas_ClienteEmail",
                table: "Cuenta",
                newName: "IX_Cuenta_ClienteEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacion",
                table: "Transacion",
                column: "TransactionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuenta",
                table: "Cuenta",
                column: "AccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuenta_Clientes_ClienteEmail",
                table: "Cuenta",
                column: "ClienteEmail",
                principalTable: "Clientes",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacion_Cuenta_CuentaAccountNumber",
                table: "Transacion",
                column: "CuentaAccountNumber",
                principalTable: "Cuenta",
                principalColumn: "AccountNumber");
        }
    }
}
