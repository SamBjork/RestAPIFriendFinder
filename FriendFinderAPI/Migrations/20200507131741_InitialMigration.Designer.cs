﻿// <auto-generated />
using System;
using FriendFinderAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FriendFinderAPI.Migrations
{
    [DbContext(typeof(FriendFinderContext))]
    [Migration("20200507131741_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FriendFinderAPI.Models.City", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityCounty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityID");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            CityID = 1,
                            CityCountry = "Sweden",
                            CityCounty = "Västra Götaland",
                            CityName = "Göteborg"
                        },
                        new
                        {
                            CityID = 2,
                            CityCountry = "Denmark",
                            CityCounty = "Malmo",
                            CityName = "Malmo"
                        },
                        new
                        {
                            CityID = 3,
                            CityCountry = "Sweden",
                            CityCounty = "Stockholm",
                            CityName = "Stockholm"
                        });
                });

            modelBuilder.Entity("FriendFinderAPI.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EventCityCityID")
                        .HasColumnType("int");

                    b.Property<int?>("EventHobbyHobbyID")
                        .HasColumnType("int");

                    b.Property<string>("EventName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EventResposibleUserID")
                        .HasColumnType("int");

                    b.HasKey("EventID");

                    b.HasIndex("EventCityCityID");

                    b.HasIndex("EventHobbyHobbyID");

                    b.HasIndex("EventResposibleUserID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("FriendFinderAPI.Models.Hobby", b =>
                {
                    b.Property<int>("HobbyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HobbyActivationLevel")
                        .HasColumnType("int");

                    b.Property<string>("HobbyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LocationID")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("HobbyID");

                    b.HasIndex("LocationID");

                    b.HasIndex("UserID");

                    b.ToTable("Hobbies");

                    b.HasData(
                        new
                        {
                            HobbyID = 1,
                            HobbyActivationLevel = 2,
                            HobbyName = "Badminton"
                        },
                        new
                        {
                            HobbyID = 2,
                            HobbyActivationLevel = 2,
                            HobbyName = "Tennis"
                        });
                });

            modelBuilder.Entity("FriendFinderAPI.Models.Location", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationID");

                    b.HasIndex("CityID");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            LocationID = 1,
                            LocationName = "Fjäderborgen"
                        });
                });

            modelBuilder.Entity("FriendFinderAPI.Models.Match", b =>
                {
                    b.Property<int>("MatchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("MatchID");

                    b.HasIndex("UserID");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("FriendFinderAPI.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserAdress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserAge")
                        .HasColumnType("int");

                    b.Property<bool>("UserIsTeacher")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            UserAdress = "Drottninggatan",
                            UserAge = 20,
                            UserIsTeacher = false,
                            UserName = "Sebbe",
                            UserPhoneNumber = "+46XXXXXXX"
                        },
                        new
                        {
                            UserID = 2,
                            UserAdress = "Kungsgatan",
                            UserAge = 22,
                            UserIsTeacher = false,
                            UserName = "Oskar",
                            UserPhoneNumber = "+46XXXXXXX2"
                        });
                });

            modelBuilder.Entity("FriendFinderAPI.Models.Event", b =>
                {
                    b.HasOne("FriendFinderAPI.Models.City", "EventCity")
                        .WithMany()
                        .HasForeignKey("EventCityCityID");

                    b.HasOne("FriendFinderAPI.Models.Hobby", "EventHobby")
                        .WithMany()
                        .HasForeignKey("EventHobbyHobbyID");

                    b.HasOne("FriendFinderAPI.Models.User", "EventResposible")
                        .WithMany()
                        .HasForeignKey("EventResposibleUserID");
                });

            modelBuilder.Entity("FriendFinderAPI.Models.Hobby", b =>
                {
                    b.HasOne("FriendFinderAPI.Models.Location", null)
                        .WithMany("Hobbies")
                        .HasForeignKey("LocationID");

                    b.HasOne("FriendFinderAPI.Models.User", null)
                        .WithMany("Hobbies")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("FriendFinderAPI.Models.Location", b =>
                {
                    b.HasOne("FriendFinderAPI.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID");
                });

            modelBuilder.Entity("FriendFinderAPI.Models.Match", b =>
                {
                    b.HasOne("FriendFinderAPI.Models.User", null)
                        .WithMany("Matches")
                        .HasForeignKey("UserID");
                });
#pragma warning restore 612, 618
        }
    }
}