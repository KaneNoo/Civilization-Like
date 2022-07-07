﻿// <auto-generated />
using System;
using CivilizationAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CivilizationAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220706073438_DefaultsAdded")]
    partial class DefaultsAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Shared.Models.Civilization.District", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CulturePoints")
                        .HasColumnType("int");

                    b.Property<int>("EraID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EraID");

                    b.HasIndex("UnitID");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("Shared.Models.Civilization.Era", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CulturePointsRequared")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Eras");
                });

            modelBuilder.Entity("Shared.Models.Civilization.Mission", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CulturePointsReward")
                        .HasColumnType("int");

                    b.Property<string>("MetaDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RealDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TypeID");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("Shared.Models.Civilization.MissionType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("MissionTypes");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Стандарт"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Дуэль"
                        });
                });

            modelBuilder.Entity("Shared.Models.Civilization.PlayerMission", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Confirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<int>("MissionID")
                        .HasColumnType("int");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("MissionID");

                    b.HasIndex("PlayerID");

                    b.ToTable("PlayersMissions");
                });

            modelBuilder.Entity("Shared.Models.Civilization.Unit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Units");

                });

            modelBuilder.Entity("Shared.Models.FortuneWheel.PlayerPrize", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.Property<int>("PrizeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PlayerID");

                    b.HasIndex("PrizeID");

                    b.ToTable("PlayersPrizes");
                });

            modelBuilder.Entity("Shared.Models.FortuneWheel.Prize", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("Chance")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Prizes");
                });

            modelBuilder.Entity("Shared.Models.FortuneWheel.Wheel", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<int>("PrizeID")
                        .HasColumnType("int");

                    b.HasKey("ID", "PrizeID");

                    b.HasIndex("PrizeID");

                    b.ToTable("Wheels");
                });

            modelBuilder.Entity("Shared.Models.Log", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Shared.Models.Player", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("DistrictID")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WheelCoins")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DistrictID")
                        .IsUnique()
                        .HasFilter("[DistrictID] IS NOT NULL");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Shared.Models.Civilization.District", b =>
                {
                    b.HasOne("Shared.Models.Civilization.Era", "Era")
                        .WithMany("Districts")
                        .HasForeignKey("EraID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Models.Civilization.Unit", "Unit")
                        .WithMany("Districts")
                        .HasForeignKey("UnitID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Era");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("Shared.Models.Civilization.Mission", b =>
                {
                    b.HasOne("Shared.Models.Civilization.MissionType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Shared.Models.Civilization.PlayerMission", b =>
                {
                    b.HasOne("Shared.Models.Player", "Player")
                        .WithMany("Missions")
                        .HasForeignKey("MissionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Models.Civilization.Mission", "Mission")
                        .WithMany("Players")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mission");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Shared.Models.FortuneWheel.PlayerPrize", b =>
                {
                    b.HasOne("Shared.Models.Player", "Player")
                        .WithMany("PlayerPrizes")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Models.FortuneWheel.Prize", "Prize")
                        .WithMany("PlayerPrizes")
                        .HasForeignKey("PrizeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Prize");
                });

            modelBuilder.Entity("Shared.Models.FortuneWheel.Wheel", b =>
                {
                    b.HasOne("Shared.Models.FortuneWheel.Prize", "Prize")
                        .WithMany("Wheels")
                        .HasForeignKey("PrizeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prize");
                });

            modelBuilder.Entity("Shared.Models.Player", b =>
                {
                    b.HasOne("Shared.Models.Civilization.District", "District")
                        .WithOne("Player")
                        .HasForeignKey("Shared.Models.Player", "DistrictID");

                    b.Navigation("District");
                });

            modelBuilder.Entity("Shared.Models.Civilization.District", b =>
                {
                    b.Navigation("Player")
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Models.Civilization.Era", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("Shared.Models.Civilization.Mission", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("Shared.Models.Civilization.Unit", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("Shared.Models.FortuneWheel.Prize", b =>
                {
                    b.Navigation("PlayerPrizes");

                    b.Navigation("Wheels");
                });

            modelBuilder.Entity("Shared.Models.Player", b =>
                {
                    b.Navigation("Missions");

                    b.Navigation("PlayerPrizes");
                });
#pragma warning restore 612, 618
        }
    }
}
