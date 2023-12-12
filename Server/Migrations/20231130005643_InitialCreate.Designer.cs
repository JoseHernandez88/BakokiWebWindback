﻿// <auto-generated />
using System;
using BakokiWeb.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BakokiWeb.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231130005643_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BakokiWeb.Shared.Cliente", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LoggedIn")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("BakokiWeb.Shared.Cuenta", b =>
                {
                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.HasKey("AccountNumber");

                    b.HasIndex("ClienteEmail");

                    b.ToTable("Cuenta");
                });

            modelBuilder.Entity("BakokiWeb.Shared.transaccion", b =>
                {
                    b.Property<long>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TransactionID"));

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<string>("CuentaAccountNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("FilledAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCredit")
                        .HasColumnType("bit");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransactionID");

                    b.HasIndex("CuentaAccountNumber");

                    b.ToTable("transaccion");
                });

            modelBuilder.Entity("BakokiWeb.Shared.Cuenta", b =>
                {
                    b.HasOne("BakokiWeb.Shared.Cliente", "Cliente")
                        .WithMany("Cuentas")
                        .HasForeignKey("ClienteEmail");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("BakokiWeb.Shared.transaccion", b =>
                {
                    b.HasOne("BakokiWeb.Shared.Cuenta", null)
                        .WithMany("transacciones")
                        .HasForeignKey("CuentaAccountNumber");
                });

            modelBuilder.Entity("BakokiWeb.Shared.Cliente", b =>
                {
                    b.Navigation("Cuentas");
                });

            modelBuilder.Entity("BakokiWeb.Shared.Cuenta", b =>
                {
                    b.Navigation("transacciones");
                });
#pragma warning restore 612, 618
        }
    }
}
