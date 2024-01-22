﻿// <auto-generated />
using CityAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityAPI.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    partial class CityInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("CityAPI.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CityDescription")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityDescription = "India's Capital",
                            CityName = "New Delhi"
                        },
                        new
                        {
                            Id = 2,
                            CityDescription = "India's Finance Hub",
                            CityName = "Mumbai"
                        },
                        new
                        {
                            Id = 3,
                            CityDescription = "India's Largest Exporter",
                            CityName = "Jamnagar"
                        });
                });

            modelBuilder.Entity("CityAPI.Entities.Place", b =>
                {
                    b.Property<int>("PlaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlaceDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("PlaceName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("PlaceId");

                    b.HasIndex("CityId");

                    b.ToTable("Places");

                    b.HasData(
                        new
                        {
                            PlaceId = 1,
                            CityId = 1,
                            PlaceDescription = "President, Lok Sabha and Rajya Sabha",
                            PlaceName = "Parliament House"
                        },
                        new
                        {
                            PlaceId = 2,
                            CityId = 1,
                            PlaceDescription = "It is UNESCO World Heritage Site.",
                            PlaceName = "Qutub Minar"
                        },
                        new
                        {
                            PlaceId = 3,
                            CityId = 2,
                            PlaceDescription = "Five Star Hotel in Mumbai",
                            PlaceName = "Hotel Taj Mahel"
                        },
                        new
                        {
                            PlaceId = 4,
                            CityId = 3,
                            PlaceDescription = "World's Largest Oil Refinary",
                            PlaceName = "Reliance Industries Limited"
                        },
                        new
                        {
                            PlaceId = 5,
                            CityId = 3,
                            PlaceDescription = "Formerly Known as Essar Oil and Essar Power Limited",
                            PlaceName = "Nyara Energy"
                        });
                });

            modelBuilder.Entity("CityAPI.Entities.Place", b =>
                {
                    b.HasOne("CityAPI.Entities.City", "City")
                        .WithMany("Places")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CityAPI.Entities.City", b =>
                {
                    b.Navigation("Places");
                });
#pragma warning restore 612, 618
        }
    }
}