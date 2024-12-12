using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPalAPI.Migrations
{
    /// <inheritdoc />
    public partial class SideEffectsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SideEffects",
                table: "Medicines");

            migrationBuilder.CreateTable(
                name: "SideEffects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Effect = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SideEffects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicineSideEffects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    SideEffectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineSideEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineSideEffects_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineSideEffects_SideEffects_SideEffectId",
                        column: x => x.SideEffectId,
                        principalTable: "SideEffects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_MedicineId",
                table: "Reminders",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineSideEffects_MedicineId",
                table: "MedicineSideEffects",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineSideEffects_SideEffectId",
                table: "MedicineSideEffects",
                column: "SideEffectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Medicines_MedicineId",
                table: "Reminders",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Users_UserId",
                table: "Reminders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Medicines_MedicineId",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Users_UserId",
                table: "Reminders");

            migrationBuilder.DropTable(
                name: "MedicineSideEffects");

            migrationBuilder.DropTable(
                name: "SideEffects");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_MedicineId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders");

            migrationBuilder.AddColumn<string>(
                name: "SideEffects",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
