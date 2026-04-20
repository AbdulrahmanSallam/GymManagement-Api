using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGymsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Gym",
                table: "Gym");

            migrationBuilder.RenameTable(
                name: "Gym",
                newName: "Gyms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gyms",
                table: "Gyms",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Gyms",
                table: "Gyms");

            migrationBuilder.RenameTable(
                name: "Gyms",
                newName: "Gym");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gym",
                table: "Gym",
                column: "Id");
        }
    }
}
