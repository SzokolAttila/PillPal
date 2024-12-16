﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PillPalAPI.Model;

#nullable disable

namespace PillPalAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241215214539_PackageSizeCreated")]
    partial class PackageSizeCreated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PillPalLib.ActiveIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ingredient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ActiveIngredients");
                });

            modelBuilder.Entity("PillPalLib.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PackageUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("PillPalLib.MedicineActiveIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActiveIngredientId")
                        .HasColumnType("int");

                    b.Property<int>("MedicineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActiveIngredientId");

                    b.HasIndex("MedicineId");

                    b.ToTable("MedicineActiveIngredients");
                });

            modelBuilder.Entity("PillPalLib.MedicineRemedyFor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MedicineId")
                        .HasColumnType("int");

                    b.Property<int>("RemedyForId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("RemedyForId");

                    b.ToTable("MedicineRemedyForAilments");
                });

            modelBuilder.Entity("PillPalLib.MedicineSideEffect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MedicineId")
                        .HasColumnType("int");

                    b.Property<int>("SideEffectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("SideEffectId");

                    b.ToTable("MedicineSideEffects");
                });

            modelBuilder.Entity("PillPalLib.PackageSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MedicineId")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.ToTable("PackageSizes");
                });

            modelBuilder.Entity("PillPalLib.RemedyFor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ailment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RemedyForAilments");
                });

            modelBuilder.Entity("PillPalLib.Reminder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("DoseCount")
                        .HasColumnType("float");

                    b.Property<int>("DoseMg")
                        .HasColumnType("int");

                    b.Property<int>("MedicineId")
                        .HasColumnType("int");

                    b.Property<string>("TakingMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("When")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("UserId");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("PillPalLib.SideEffect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Effect")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SideEffects");
                });

            modelBuilder.Entity("PillPalLib.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PillPalLib.MedicineActiveIngredient", b =>
                {
                    b.HasOne("PillPalLib.ActiveIngredient", "ActiveIngredient")
                        .WithMany("ActiveIngredients")
                        .HasForeignKey("ActiveIngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PillPalLib.Medicine", "Medicine")
                        .WithMany("ActiveIngredients")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActiveIngredient");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("PillPalLib.MedicineRemedyFor", b =>
                {
                    b.HasOne("PillPalLib.Medicine", "Medicine")
                        .WithMany("RemedyForAilments")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PillPalLib.RemedyFor", "RemedyFor")
                        .WithMany("MedicineRemedyForAilments")
                        .HasForeignKey("RemedyForId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");

                    b.Navigation("RemedyFor");
                });

            modelBuilder.Entity("PillPalLib.MedicineSideEffect", b =>
                {
                    b.HasOne("PillPalLib.Medicine", "Medicine")
                        .WithMany("SideEffects")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PillPalLib.SideEffect", "SideEffect")
                        .WithMany("MedicineSideEffects")
                        .HasForeignKey("SideEffectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");

                    b.Navigation("SideEffect");
                });

            modelBuilder.Entity("PillPalLib.PackageSize", b =>
                {
                    b.HasOne("PillPalLib.Medicine", null)
                        .WithMany("PackageSizes")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PillPalLib.Reminder", b =>
                {
                    b.HasOne("PillPalLib.Medicine", "Medicine")
                        .WithMany("Reminders")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PillPalLib.User", "User")
                        .WithMany("Reminders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PillPalLib.ActiveIngredient", b =>
                {
                    b.Navigation("ActiveIngredients");
                });

            modelBuilder.Entity("PillPalLib.Medicine", b =>
                {
                    b.Navigation("ActiveIngredients");

                    b.Navigation("PackageSizes");

                    b.Navigation("RemedyForAilments");

                    b.Navigation("Reminders");

                    b.Navigation("SideEffects");
                });

            modelBuilder.Entity("PillPalLib.RemedyFor", b =>
                {
                    b.Navigation("MedicineRemedyForAilments");
                });

            modelBuilder.Entity("PillPalLib.SideEffect", b =>
                {
                    b.Navigation("MedicineSideEffects");
                });

            modelBuilder.Entity("PillPalLib.User", b =>
                {
                    b.Navigation("Reminders");
                });
#pragma warning restore 612, 618
        }
    }
}