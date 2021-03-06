﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projektarbete_VäderdataDB.Models;

namespace Projektarbete_VäderdataDB.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20210121192638_20210119090151_PROJ20210119_20210121_v002")]
    partial class _20210119090151_PROJ20210119_20210121_v002
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Projektarbete_VäderdataDB.Models.Temperature", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Luftfuktighet")
                        .HasColumnType("int");

                    b.Property<string>("Plats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Temperatur")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.ToTable("Temperatures");
                });
#pragma warning restore 612, 618
        }
    }
}
