using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PillPalAPI.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedSeeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ActiveIngredients",
                columns: new[] { "Id", "Ingredient" },
                values: new object[,]
                {
                    { 6, "Amoxicillin" },
                    { 7, "Klavulánsav" },
                    { 8, "Kolekalciferol" },
                    { 9, "Azitormicin" },
                    { 10, "Acetilszalicilsav" },
                    { 11, "Aszkorbinsav" }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Description", "Manufacturer", "Name", "PackageUnitId" },
                values: new object[,]
                {
                    { 6, "Széles spektrumú antibiotikum, bakteriális fertőzések kezelésére, mint például légúti, húgyúti és bőrfertőzések.", "Teva", "Amoxicillin 750 mg", 1 },
                    { 7, "Széles spektrumú antibiotikum, bakteriális fertőzések kezelésére, mint például légúti, húgyúti és bőrfertőzések.", "Teva", "Amoxicillin 1000 mg", 1 },
                    { 9, "Hozzájárul az egészséges csontozat, fogazat és izomfunkciók fenntartásához, emellett szerepet játszik az immunrendszer megfelelő működésében is.", "Béres", "Béres VITA-D3 1600 NE", 1 },
                    { 11, "Hozzájárul az egészséges csontozat, fogazat és izomfunkciók fenntartásához, emellett szerepet játszik az immunrendszer megfelelő működésében is.", "Béres", "Béres VITA-D3 FORTE 3200 NE", 1 },
                    { 12, "Fertőző betegségek kezelésére szolgáló antibiotikum, ami gátolja a baktériumok fehérjeszintézisét, ezáltal megakadályozza a baktériumok növekedését és szaporodását.", "Teva", "Zitrocin 500 mg", 1 },
                    { 13, "Mikroaktív technológiájának köszönhetően a leggyorsabb felszívódású Aspirin. Hatékonyan csillapítja a fejfájást és a lázat.", "Bayer", "Aspirin Ultra", 1 },
                    { 17, "Hatékonyan enyhíti a megfázás első jeleinek fájdalmait, csillapítja a lázat és még C-vitamint is tartalmaz.", "Bayer", "Aspirin Plus C", 1 },
                    { 18, "Hatékonyan enyhíti az erősebb megfázáshoz kapcsolódó fájdalmakat is, csillapítja a lázat és még C-vitamint is tartalmaz. Dupla hatóanyagtartalommal!", "Bayer", "Aspirin Plus C Forte", 1 },
                    { 19, "Megakadályozza a vérlemezkék összetapadását, s ezáltal az ereket szűkítő vagy elzáró vérrögök képződését.", "Bayer", "Aspirin Protect", 1 }
                });

            migrationBuilder.InsertData(
                table: "PackageUnits",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "kapszula" },
                    { 5, "tasak" }
                });

            migrationBuilder.InsertData(
                table: "RemedyForAilments",
                columns: new[] { "Id", "Ailment" },
                values: new object[,]
                {
                    { 6, "Légúti fertőzések" },
                    { 7, "Húgyúti fertőzések" },
                    { 8, "Bőrfertőzések" },
                    { 9, "D-vitamin-hiány" },
                    { 10, "Csontritkulás" },
                    { 11, "Angolkór" },
                    { 12, "Nemi úton terjedő betegségek" },
                    { 13, "Fejfájás" },
                    { 14, "Megfázás" },
                    { 15, "Láz" },
                    { 16, "Torokfájás" }
                });

            migrationBuilder.InsertData(
                table: "SideEffects",
                columns: new[] { "Id", "Effect" },
                values: new object[,]
                {
                    { 6, "Émelygés" },
                    { 7, "Magas kálciumszint" },
                    { 8, "Hasi fájdalom" },
                    { 9, "Vérzékenység" }
                });

            migrationBuilder.InsertData(
                table: "MedicineActiveIngredients",
                columns: new[] { "Id", "ActiveIngredientId", "MedicineId" },
                values: new object[,]
                {
                    { 6, 6, 6 },
                    { 7, 7, 6 },
                    { 8, 6, 7 },
                    { 9, 7, 7 },
                    { 11, 8, 9 },
                    { 13, 8, 11 },
                    { 14, 9, 12 },
                    { 15, 10, 13 },
                    { 19, 10, 17 },
                    { 20, 11, 17 },
                    { 21, 10, 18 },
                    { 22, 11, 18 },
                    { 23, 10, 19 }
                });

            migrationBuilder.InsertData(
                table: "MedicineRemedyForAilments",
                columns: new[] { "Id", "MedicineId", "RemedyForId" },
                values: new object[,]
                {
                    { 6, 6, 6 },
                    { 7, 6, 7 },
                    { 8, 6, 8 },
                    { 9, 7, 6 },
                    { 10, 7, 7 },
                    { 11, 7, 8 },
                    { 15, 9, 9 },
                    { 16, 9, 10 },
                    { 17, 9, 11 },
                    { 21, 11, 9 },
                    { 22, 11, 10 },
                    { 23, 11, 11 },
                    { 24, 12, 6 },
                    { 25, 12, 7 },
                    { 26, 12, 8 },
                    { 27, 12, 12 },
                    { 28, 13, 13 },
                    { 34, 17, 13 },
                    { 35, 17, 15 },
                    { 36, 17, 16 },
                    { 37, 18, 13 },
                    { 38, 18, 15 },
                    { 39, 18, 16 },
                    { 40, 19, 4 }
                });

            migrationBuilder.InsertData(
                table: "MedicineSideEffects",
                columns: new[] { "Id", "MedicineId", "SideEffectId" },
                values: new object[,]
                {
                    { 11, 6, 4 },
                    { 12, 6, 5 },
                    { 13, 6, 6 },
                    { 14, 7, 4 },
                    { 15, 7, 5 },
                    { 16, 7, 6 },
                    { 18, 9, 7 },
                    { 20, 11, 7 },
                    { 21, 12, 2 },
                    { 22, 12, 5 },
                    { 23, 12, 8 },
                    { 24, 13, 8 },
                    { 25, 13, 9 },
                    { 32, 17, 8 },
                    { 33, 17, 9 },
                    { 34, 18, 8 },
                    { 35, 18, 9 },
                    { 36, 19, 8 },
                    { 37, 19, 9 }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Description", "Manufacturer", "Name", "PackageUnitId" },
                values: new object[,]
                {
                    { 8, "Hozzájárul az egészséges csontozat, fogazat és izomfunkciók fenntartásához, emellett szerepet játszik az immunrendszer megfelelő működésében is.", "Béres", "Béres VITA-D3 4000 NE", 4 },
                    { 10, "Hozzájárul az egészséges csontozat, fogazat és izomfunkciók fenntartásához, emellett szerepet játszik az immunrendszer megfelelő működésében is.", "Béres", "Béres VITA-D3 2000 NE", 4 },
                    { 14, "Enyhíti a megfázás okozta fájdalmakat, csillapítja a lázat és még az orrdugulást is megszünteti.", "Bayer", "Aspirin Complex", 5 },
                    { 15, "Enyhíti a megfázás okozta fájdalmakat, csillapítja a lázat és még az orrdugulást is megszünteti.", "Bayer", "Aspirin Complex Forró ital", 5 },
                    { 16, "A fejfájás nem válogat, bárhol, bármikor megjelenhet. Készüljön fel rá az Aspirin Effect-tel!", "Bayer", "Aspirin Effect", 5 }
                });

            migrationBuilder.InsertData(
                table: "PackageSizes",
                columns: new[] { "Id", "MedicineId", "Size" },
                values: new object[,]
                {
                    { 6, 6, 10 },
                    { 7, 6, 20 },
                    { 8, 7, 10 },
                    { 9, 7, 20 },
                    { 11, 9, 90 },
                    { 13, 11, 60 },
                    { 14, 11, 120 },
                    { 15, 12, 3 },
                    { 16, 13, 8 },
                    { 20, 17, 10 },
                    { 21, 18, 10 },
                    { 22, 19, 28 }
                });

            migrationBuilder.InsertData(
                table: "MedicineActiveIngredients",
                columns: new[] { "Id", "ActiveIngredientId", "MedicineId" },
                values: new object[,]
                {
                    { 10, 8, 8 },
                    { 12, 8, 10 },
                    { 16, 10, 14 },
                    { 17, 10, 15 },
                    { 18, 10, 16 }
                });

            migrationBuilder.InsertData(
                table: "MedicineRemedyForAilments",
                columns: new[] { "Id", "MedicineId", "RemedyForId" },
                values: new object[,]
                {
                    { 12, 8, 9 },
                    { 13, 8, 10 },
                    { 14, 8, 11 },
                    { 18, 10, 9 },
                    { 19, 10, 10 },
                    { 20, 10, 11 },
                    { 29, 14, 14 },
                    { 30, 14, 15 },
                    { 31, 15, 14 },
                    { 32, 15, 15 },
                    { 33, 16, 13 }
                });

            migrationBuilder.InsertData(
                table: "MedicineSideEffects",
                columns: new[] { "Id", "MedicineId", "SideEffectId" },
                values: new object[,]
                {
                    { 17, 8, 7 },
                    { 19, 10, 7 },
                    { 26, 14, 8 },
                    { 27, 14, 9 },
                    { 28, 15, 8 },
                    { 29, 15, 9 },
                    { 30, 16, 8 },
                    { 31, 16, 9 }
                });

            migrationBuilder.InsertData(
                table: "PackageSizes",
                columns: new[] { "Id", "MedicineId", "Size" },
                values: new object[,]
                {
                    { 10, 8, 60 },
                    { 12, 10, 120 },
                    { 17, 14, 10 },
                    { 18, 15, 10 },
                    { 19, 16, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MedicineActiveIngredients",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "MedicineRemedyForAilments",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "MedicineSideEffects",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "PackageSizes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ActiveIngredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ActiveIngredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ActiveIngredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ActiveIngredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ActiveIngredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ActiveIngredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RemedyForAilments",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SideEffects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SideEffects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SideEffects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SideEffects",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PackageUnits",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PackageUnits",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
