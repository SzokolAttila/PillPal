using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPalAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemedyForCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemedyFor",
                table: "Medicines");

            migrationBuilder.CreateTable(
                name: "RemedyForAilments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ailment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemedyForAilments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemedyForAilments");

            migrationBuilder.AddColumn<string>(
                name: "RemedyFor",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
