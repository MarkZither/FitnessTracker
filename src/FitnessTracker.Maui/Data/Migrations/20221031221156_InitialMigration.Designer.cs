﻿// <auto-generated />
using System;
using FitnessTracker.Maui.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FitnessTracker.Maui.Data.Migrations
{
    [DbContext(typeof(TrackerContext))]
    [Migration("20221031221156_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("FitnessTracker.Maui.Data.Route", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Route", (string)null);
                });

            modelBuilder.Entity("FitnessTracker.Maui.Data.TrackerLocation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Accuracy")
                        .HasColumnType("REAL");

                    b.Property<double?>("Altitude")
                        .HasColumnType("REAL");

                    b.Property<int>("AltitudeReferenceSystem")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Course")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsFromMockProvider")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<bool>("ReducedAccuracy")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Routeid")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Speed")
                        .HasColumnType("REAL");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<double?>("VerticalAccuracy")
                        .HasColumnType("REAL");

                    b.HasKey("id");

                    b.HasIndex("Routeid");

                    b.ToTable("TrackerLocation");
                });

            modelBuilder.Entity("FitnessTracker.Maui.Data.TrackerLocation", b =>
                {
                    b.HasOne("FitnessTracker.Maui.Data.Route", null)
                        .WithMany("Locations")
                        .HasForeignKey("Routeid");
                });

            modelBuilder.Entity("FitnessTracker.Maui.Data.Route", b =>
                {
                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}