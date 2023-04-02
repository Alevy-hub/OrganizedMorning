﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrganizedMorning.OrganizedMorning;

#nullable disable

namespace OrganizedMorning.Migrations
{
    [DbContext(typeof(OrganizedMorningDbContext))]
    [Migration("20230330124316_Initneww")]
    partial class Initneww
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrganizedMorning.Entities.MorningPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan?>("BaseTime")
                        .HasColumnType("time");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("EncodedTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MorningPlans");
                });

            modelBuilder.Entity("OrganizedMorning.Entities.Times", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EncodedTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MorningPlanId")
                        .HasColumnType("int");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("Time")
                        .HasColumnType("time");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MorningPlanId");

                    b.ToTable("Times");
                });

            modelBuilder.Entity("OrganizedMorning.Entities.Times", b =>
                {
                    b.HasOne("OrganizedMorning.Entities.MorningPlan", "MorningPlan")
                        .WithMany()
                        .HasForeignKey("MorningPlanId");

                    b.Navigation("MorningPlan");
                });
#pragma warning restore 612, 618
        }
    }
}