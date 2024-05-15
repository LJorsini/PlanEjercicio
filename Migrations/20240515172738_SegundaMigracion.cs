using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanEjercicio.Migrations
{
    /// <inheritdoc />
    public partial class SegundaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EjerciciosFisicos_TipoEjercicios_TipoEjercicioIdEjercicio",
                table: "EjerciciosFisicos");

            migrationBuilder.DropIndex(
                name: "IX_EjerciciosFisicos_TipoEjercicioIdEjercicio",
                table: "EjerciciosFisicos");

            migrationBuilder.DropColumn(
                name: "TipoEjercicioIdEjercicio",
                table: "EjerciciosFisicos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoEjercicioIdEjercicio",
                table: "EjerciciosFisicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EjerciciosFisicos_TipoEjercicioIdEjercicio",
                table: "EjerciciosFisicos",
                column: "TipoEjercicioIdEjercicio");

            migrationBuilder.AddForeignKey(
                name: "FK_EjerciciosFisicos_TipoEjercicios_TipoEjercicioIdEjercicio",
                table: "EjerciciosFisicos",
                column: "TipoEjercicioIdEjercicio",
                principalTable: "TipoEjercicios",
                principalColumn: "IdEjercicio",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
