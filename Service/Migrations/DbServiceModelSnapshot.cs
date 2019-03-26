﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistance;

namespace Service.Migrations
{
    [DbContext(typeof(DbService))]
    partial class DbServiceModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DevChallenge.Domain.InstrumentPrice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Instrument")
                        .IsRequired();

                    b.Property<string>("InstrumentOwner")
                        .IsRequired();

                    b.Property<string>("Portfolio")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<int>("TimeSlot");

                    b.HasKey("Id");

                    b.HasIndex("TimeSlot");

                    b.ToTable("InstrumentPrices");
                });
#pragma warning restore 612, 618
        }
    }
}
