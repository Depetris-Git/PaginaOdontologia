using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Odontología.DB.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTelefono = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    NumeroTelefonoSecundario = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoTratamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTratamientos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TratamientoOdId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PresupuestoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Presupuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoPres = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CostoIncial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoAbonado = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostoPorPagar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pagado = table.Column<bool>(type: "bit", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    PagoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuestos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presupuestos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Presupuestos_Pagos_PagoId",
                        column: x => x.PagoId,
                        principalTable: "Pagos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TratamientosOd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostoAcordado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoProtesista = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CostoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    TipoTratamientoId = table.Column<int>(type: "int", nullable: false),
                    PresupuestoId = table.Column<int>(type: "int", nullable: true),
                    FechaOperacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(900)", maxLength: 900, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TratamientosOd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TratamientosOd_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TratamientosOd_Presupuestos_PresupuestoId",
                        column: x => x.PresupuestoId,
                        principalTable: "Presupuestos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TratamientosOd_TipoTratamientos_TipoTratamientoId",
                        column: x => x.TipoTratamientoId,
                        principalTable: "TipoTratamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "DNI_UQ",
                table: "Pacientes",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_PresupuestoId",
                table: "Pagos",
                column: "PresupuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_TratamientoOdId",
                table: "Pagos",
                column: "TratamientoOdId");

            migrationBuilder.CreateIndex(
                name: "CodigoPres_UQ",
                table: "Presupuestos",
                column: "CodigoPres",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Presupuestos_PacienteId",
                table: "Presupuestos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuestos_PagoId",
                table: "Presupuestos",
                column: "PagoId");

            migrationBuilder.CreateIndex(
                name: "TipoTrat_Nombre_UQ",
                table: "TipoTratamientos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TratamientosOd_PacienteId",
                table: "TratamientosOd",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamientosOd_PresupuestoId",
                table: "TratamientosOd",
                column: "PresupuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamientosOd_TipoTratamientoId",
                table: "TratamientosOd",
                column: "TipoTratamientoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Presupuestos_PresupuestoId",
                table: "Pagos",
                column: "PresupuestoId",
                principalTable: "Presupuestos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_TratamientosOd_TratamientoOdId",
                table: "Pagos",
                column: "TratamientoOdId",
                principalTable: "TratamientosOd",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Presupuestos_PresupuestoId",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_TratamientosOd_Presupuestos_PresupuestoId",
                table: "TratamientosOd");

            migrationBuilder.DropTable(
                name: "Presupuestos");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "TratamientosOd");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "TipoTratamientos");
        }
    }
}
