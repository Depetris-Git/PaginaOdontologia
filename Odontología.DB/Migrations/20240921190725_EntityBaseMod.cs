using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Odontología.DB.Migrations
{
    /// <inheritdoc />
    public partial class EntityBaseMod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "TratamientosOd",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "TipoTratamientos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Presupuestos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Pagos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Pacientes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "TratamientosOd");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "TipoTratamientos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Presupuestos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Pacientes");
        }
    }
}
