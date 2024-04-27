using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanEjercicio.Migrations
{
    /// <inheritdoc />
    public partial class TablaEjercicioFisico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "TipoEjercicios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "EjercicioFisico",
                columns: table => new
                {
                    EjercicioFisicoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEjercicio = table.Column<int>(type: "int", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoEmocionalInicio = table.Column<int>(type: "int", nullable: false),
                    EstadoEmocionalFin = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoEjercicioIdEjercicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EjercicioFisico", x => x.EjercicioFisicoId);
                    table.ForeignKey(
                        name: "FK_EjercicioFisico_TipoEjercicios_TipoEjercicioIdEjercicio",
                        column: x => x.TipoEjercicioIdEjercicio,
                        principalTable: "TipoEjercicios",
                        principalColumn: "IdEjercicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EjercicioFisico_TipoEjercicioIdEjercicio",
                table: "EjercicioFisico",
                column: "TipoEjercicioIdEjercicio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EjercicioFisico");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "TipoEjercicios");
        }
    }
}
