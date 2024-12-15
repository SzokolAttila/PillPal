using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPalAPI.Migrations
{
    /// <inheritdoc />
    public partial class MedicineRemedyForCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicineRemedyForAilments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    RemedyForId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineRemedyForAilments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineRemedyForAilments_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineRemedyForAilments_RemedyForAilments_RemedyForId",
                        column: x => x.RemedyForId,
                        principalTable: "RemedyForAilments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineRemedyForAilments_MedicineId",
                table: "MedicineRemedyForAilments",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineRemedyForAilments_RemedyForId",
                table: "MedicineRemedyForAilments",
                column: "RemedyForId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineRemedyForAilments");
        }
    }
}
