﻿// <auto-generated />
using System;
using JukeBoxPartyAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JukeBoxPartyAPI.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230326182645_QueueElementUpdated")]
    partial class QueueElementUpdated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JukeBoxPartyAPI.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Pop"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Rock"
                        },
                        new
                        {
                            Id = 3,
                            Title = "Hip-hop"
                        },
                        new
                        {
                            Id = 4,
                            Title = "Rap"
                        },
                        new
                        {
                            Id = 5,
                            Title = "Country"
                        },
                        new
                        {
                            Id = 6,
                            Title = "R&B"
                        },
                        new
                        {
                            Id = 7,
                            Title = "Soul"
                        },
                        new
                        {
                            Id = 8,
                            Title = "Electronic"
                        },
                        new
                        {
                            Id = 9,
                            Title = "Dance"
                        },
                        new
                        {
                            Id = 10,
                            Title = "Jazz"
                        },
                        new
                        {
                            Id = 11,
                            Title = "Blues"
                        },
                        new
                        {
                            Id = 12,
                            Title = "Classical"
                        },
                        new
                        {
                            Id = 13,
                            Title = "Reggae"
                        },
                        new
                        {
                            Id = 14,
                            Title = "Punk"
                        },
                        new
                        {
                            Id = 15,
                            Title = "Metal"
                        },
                        new
                        {
                            Id = 16,
                            Title = "Folk"
                        },
                        new
                        {
                            Id = 17,
                            Title = "World Music"
                        },
                        new
                        {
                            Id = 18,
                            Title = "Funk"
                        },
                        new
                        {
                            Id = 19,
                            Title = "Gospel"
                        },
                        new
                        {
                            Id = 20,
                            Title = "Alternative"
                        },
                        new
                        {
                            Id = 21,
                            Title = "Indie"
                        },
                        new
                        {
                            Id = 22,
                            Title = "Latin"
                        },
                        new
                        {
                            Id = 23,
                            Title = "Opera"
                        });
                });

            modelBuilder.Entity("JukeBoxPartyAPI.Models.Lobby", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Lobbies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("074950e4-7108-45bb-8144-cd6c60e7bc26"),
                            CreatedAt = new DateTime(2023, 3, 26, 20, 26, 44, 856, DateTimeKind.Local).AddTicks(148),
                            IsActive = true
                        },
                        new
                        {
                            Id = new Guid("7de18949-4e72-4967-ba3a-8f6d885f2ff7"),
                            CreatedAt = new DateTime(2023, 3, 26, 20, 26, 44, 856, DateTimeKind.Local).AddTicks(208),
                            IsActive = true
                        },
                        new
                        {
                            Id = new Guid("c6614c17-9455-4124-922c-ddcac7e0d8b4"),
                            CreatedAt = new DateTime(2023, 3, 26, 20, 26, 44, 856, DateTimeKind.Local).AddTicks(211),
                            IsActive = true
                        });
                });

            modelBuilder.Entity("JukeBoxPartyAPI.Models.QueueElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LobbyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("PlayedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LobbyId");

                    b.HasIndex("SongId");

                    b.ToTable("QueueElements");
                });

            modelBuilder.Entity("JukeBoxPartyAPI.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("Songs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Artist = "Alex-Productions",
                            Duration = 154000.0,
                            GenreId = 17,
                            Title = "Freedom",
                            URL = "track1.mp3"
                        },
                        new
                        {
                            Id = 2,
                            Artist = "Roa",
                            Duration = 156000.0,
                            GenreId = 17,
                            Title = "Sunset Walk",
                            URL = "track2.mp3"
                        },
                        new
                        {
                            Id = 3,
                            Artist = "LiQWYD",
                            Duration = 135000.0,
                            GenreId = 17,
                            Title = "The Light",
                            URL = "track3.mp3"
                        });
                });

            modelBuilder.Entity("JukeBoxPartyAPI.Models.QueueElement", b =>
                {
                    b.HasOne("JukeBoxPartyAPI.Models.Lobby", "Lobby")
                        .WithMany()
                        .HasForeignKey("LobbyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JukeBoxPartyAPI.Models.Song", "Song")
                        .WithMany()
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lobby");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("JukeBoxPartyAPI.Models.Song", b =>
                {
                    b.HasOne("JukeBoxPartyAPI.Models.Genre", "Genre")
                        .WithMany("Songs")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("JukeBoxPartyAPI.Models.Genre", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}