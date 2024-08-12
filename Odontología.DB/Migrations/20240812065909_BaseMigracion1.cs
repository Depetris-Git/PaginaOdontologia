using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Odontología.DB.Migrations
{
    /// <inheritdoc />
    public partial class BaseMigracion1 : Migration
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
                    NumeroTelefonoSecundario = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
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
                    CostoAbonado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "TratamientosOd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoTratamiento = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CostoAcordado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoProtesista = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoPagadoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoPorPagar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    FechaOperacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pagado = table.Column<bool>(type: "bit", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TratamientoOdId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagos_TratamientosOd_TratamientoOdId",
                        column: x => x.TratamientoOdId,
                        principalTable: "TratamientosOd",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "DNI_UQ",
                table: "Pacientes",
                column: "DNI",
                unique: true);

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
                name: "IX_TratamientosOd_PacienteId",
                table: "TratamientosOd",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Presupuestos");

            migrationBuilder.DropTable(
                name: "TratamientosOd");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
