﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Odontología.DB.Data;

#nullable disable

namespace Odontología.DB.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240812065909_BaseMigracion1")]
    partial class BaseMigracion1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Odontología.DB.Data.Entity.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroTelefono")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("NumeroTelefonoSecundario")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "DNI" }, "DNI_UQ")
                        .IsUnique();

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Odontología.DB.Data.Entity.Pago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TratamientoOdId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TratamientoOdId");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("Odontología.DB.Data.Entity.Presupuesto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodigoPres")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<decimal>("CostoAbonado")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostoIncial")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.HasIndex(new[] { "CodigoPres" }, "CodigoPres_UQ")
                        .IsUnique();

                    b.ToTable("Presupuestos");
                });

            modelBuilder.Entity("Odontología.DB.Data.Entity.TratamientoOd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CostoAcordado")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostoPagadoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostoPorPagar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostoProtesista")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("FechaOperacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(900)
                        .HasColumnType("nvarchar(900)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<bool>("Pagado")
                        .HasColumnType("bit");

                    b.Property<string>("TipoTratamiento")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.ToTable("TratamientosOd");
                });

            modelBuilder.Entity("Odontología.DB.Data.Entity.Pago", b =>
                {
                    b.HasOne("Odontología.DB.Data.Entity.TratamientoOd", "TratamientoOd")
                        .WithMany("Pagos")
                        .HasForeignKey("TratamientoOdId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TratamientoOd");
                });

            modelBuilder.Entity("Odontología.DB.Data.Entity.Presupuesto", b =>
                {
                    b.HasOne("Odontología.DB.Data.Entity.Paciente", "Paciente")
                        .WithMany("Presupuestos")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Odontología.DB.Data.Entity.TratamientoOd", b =>
                {
                    b.HasOne("Odontología.DB.Data.Entity.Paciente", "Paciente")
                        .WithMany("TratamientosOd")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Odontología.DB.Data.Entity.Paciente", b =>
                {
                    b.Navigation("Presupuestos");

                    b.Navigation("TratamientosOd");
                });

            modelBuilder.Entity("Odontología.DB.Data.Entity.TratamientoOd", b =>
                {
                    b.Navigation("Pagos");
                });
#pragma warning restore 612, 618
        }
    }
}
