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
    [Migration("20210123173130_Updated_Tables_20210123_v001")]
    partial class Updated_Tables_20210123_v001
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

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("Datum");

                    b.Property<int?>("Humidity")
                        .HasColumnType("int")
                        .HasColumnName("Luftfuktighet");

                    b.Property<string>("Place")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("Plats");

                    b.Property<decimal?>("Temp")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Temperatur");

                    b.HasKey("ID");

                    b.ToTable("Temperatures");
                });
#pragma warning restore 612, 618
        }
    }
}
