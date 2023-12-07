using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakokiWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoggedIn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    ClienteEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_Cuenta_Clientes_ClienteEmail",
                        column: x => x.ClienteEmail,
                        principalTable: "Clientes",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Transacion",
                columns: table => new
                {
                    TransactionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    IsCredit = table.Column<bool>(type: "bit", nullable: false),
                    FilledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CuentaAccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacion", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transacion_Cuenta_CuentaAccountNumber",
                        column: x => x.CuentaAccountNumber,
                        principalTable: "Cuenta",
                        principalColumn: "AccountNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_ClienteEmail",
                table: "Cuenta",
                column: "ClienteEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Transacion_CuentaAccountNumber",
                table: "Transacion",
                column: "CuentaAccountNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacion");

            migrationBuilder.DropTable(
                name: "Cuenta");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
