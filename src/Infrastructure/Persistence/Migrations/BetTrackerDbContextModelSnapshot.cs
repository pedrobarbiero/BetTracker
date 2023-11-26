﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(BetTrackerDbContext))]
    partial class BetTrackerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Bet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BettingMarketId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BrankroolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("PreMatch")
                        .HasColumnType("bit");

                    b.Property<bool>("Settled")
                        .HasColumnType("bit");

                    b.Property<Guid?>("TipsterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalReturn")
                        .HasColumnType("decimal(10, 4)");

                    b.Property<decimal>("TotalStake")
                        .HasColumnType("decimal(10, 4)");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("BettingMarketId");

                    b.HasIndex("TipsterId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("Domain.BettingMarket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Sport")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("BettingMarket");
                });

            modelBuilder.Entity("Domain.Pick", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("Odd")
                        .HasColumnType("decimal(10, 4)");

                    b.Property<int>("Sport")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("BetId");

                    b.ToTable("Pick");
                });

            modelBuilder.Entity("Domain.Tipster", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Tipster");
                });

            modelBuilder.Entity("Domain.Bet", b =>
                {
                    b.HasOne("Domain.BettingMarket", "BettingMarket")
                        .WithMany()
                        .HasForeignKey("BettingMarketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Tipster", "Tipster")
                        .WithMany("Bets")
                        .HasForeignKey("TipsterId");

                    b.Navigation("BettingMarket");

                    b.Navigation("Tipster");
                });

            modelBuilder.Entity("Domain.Pick", b =>
                {
                    b.HasOne("Domain.Bet", "Bet")
                        .WithMany("Picks")
                        .HasForeignKey("BetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bet");
                });

            modelBuilder.Entity("Domain.Bet", b =>
                {
                    b.Navigation("Picks");
                });

            modelBuilder.Entity("Domain.Tipster", b =>
                {
                    b.Navigation("Bets");
                });
#pragma warning restore 612, 618
        }
    }
}
