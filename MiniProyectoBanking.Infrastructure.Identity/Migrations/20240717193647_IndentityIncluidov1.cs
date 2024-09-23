using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProyectoBanking.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class IndentityIncluidov1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "Identity",
                table: "Usuarios",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "Identity",
                table: "Usuarios",
                newName: "Cedula");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                schema: "Identity",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                schema: "Identity",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                schema: "Identity",
                table: "Usuarios",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Cedula",
                schema: "Identity",
                table: "Usuarios",
                newName: "FirstName");
        }
    }
}
