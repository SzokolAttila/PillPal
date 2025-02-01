using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPalAPI.Migrations
{
    /// <inheritdoc />
    public partial class PackageUnitModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageUnit",
                table: "Medicines");

            migrationBuilder.AddColumn<int>(
                name: "PackageUnitId",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PackageUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageUnits", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_PackageUnitId",
                table: "Medicines",
                column: "PackageUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_PackageUnits_PackageUnitId",
                table: "Medicines",
                column: "PackageUnitId",
                principalTable: "PackageUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_PackageUnits_PackageUnitId",
                table: "Medicines");

            migrationBuilder.DropTable(
                name: "PackageUnits");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_PackageUnitId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "PackageUnitId",
                table: "Medicines");

            migrationBuilder.AddColumn<string>(
                name: "PackageUnit",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
