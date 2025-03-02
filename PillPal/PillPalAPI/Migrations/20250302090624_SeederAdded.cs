using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PillPalAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeederAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoseMg",
                table: "Reminders");

            migrationBuilder.InsertData(
                table: "ActiveIngredients",
                columns: new[] { "Id", "Ingredient" },
                values: new object[,]
                {
                    { 1, "Adalimumab" },
                    { 2, "Pembrolizumab" },
                    { 3, "Semaglutid" },
                    { 4, "Apixaban" },
                    { 5, "Dupilumab" }
                });

            migrationBuilder.InsertData(
                table: "PackageUnits",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "tabletta" },
                    { 2, "ml" },
                    { 3, "injekció" }
                });

            migrationBuilder.InsertData(
                table: "RemedyForAilments",
                columns: new[] { "Id", "Ailment" },
                values: new object[,]
                {
                    { 1, "Reumatoid artritisz" },
                    { 2, "Melanóma" },
                    { 3, "2-es típusú cukorbetegség" },
                    { 4, "Vérrögképződés" },
                    { 5, "Ekcéma" }
                });

            migrationBuilder.InsertData(
                table: "SideEffects",
                columns: new[] { "Id", "Effect" },
                values: new object[,]
                {
                    { 1, "Fejfájás" },
                    { 2, "Hányinger" },
                    { 3, "Fáradtság" },
                    { 4, "Bőrkiütés" },
                    { 5, "Hasmenés" }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Description", "Manufacturer", "Name", "PackageUnitId" },
                values: new object[,]
                {
                    { 1, "Autoimmun betegségek kezelésére szolgáló biológiai gyógyszer.", "AbbVie", "Humira", 1 },
                    { 2, "Immunterápiás gyógyszer különböző rákos megbetegedések kezelésére.", "Merck", "Keytruda", 2 },
                    { 3, "2-es típusú cukorbetegség és testsúlycsökkentés kezelésére szolgáló gyógyszer.", "Novo Nordisk", "Ozempic", 1 },
                    { 4, "Antikoaguláns, amely a vérrögképződést gátolja.", "Bristol-Myers Squibb és Pfizer", "Eliquis", 1 },
                    { 5, "Biológiai gyógyszer gyulladásos betegségek kezelésére.", "Sanofi és Regeneron", "Dupixent", 3 }
                });

            migrationBuilder.InsertData(
                table: "MedicineActiveIngredients",
                columns: new[] { "Id", "ActiveIngredientId", "MedicineId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "MedicineRemedyForAilments",
                columns: new[] { "Id", "MedicineId", "RemedyForId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "MedicineSideEffects",
                columns: new[] { "Id", "MedicineId", "SideEffectId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 4 },
                    { 5, 3, 5 },
                    { 6, 3, 1 },
                    { 7, 4, 2 },
                    { 8, 4, 3 },
                    { 9, 5, 4 },
                    { 10, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "PackageSizes",
                columns: new[] { "Id", "MedicineId", "Size" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 4 },
                    { 3, 3, 1 },
                    { 4, 4, 10 },
                    { 5, 5, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ActiveIngredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ActiveIngredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ActiveIngredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ActiveIngredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ActiveIngredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SideEffects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SideEffects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SideEffects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SideEffects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SideEffects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PackageUnits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PackageUnits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PackageUnits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "DoseMg",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
