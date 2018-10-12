﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScrubBot.Database;

namespace ScrubBot.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20181003204455_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("ScrubBot.Database.Models.Guild", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuditChannelId");

                    b.Property<string>("CharPrefix");

                    b.Property<string>("IconUrl");

                    b.Property<int>("MemberCount");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("StringPrefix");

                    b.HasKey("Id");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("ScrubBot.Database.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<string>("AvatarUrl");

                    b.Property<string>("Discriminator")
                        .HasMaxLength(20);

                    b.Property<string>("GuildId");

                    b.Property<string>("Nickname")
                        .HasMaxLength(50);

                    b.Property<int?>("TimezoneOffset");

                    b.Property<string>("Username")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ScrubBot.Database.Models.User", b =>
                {
                    b.HasOne("ScrubBot.Database.Models.Guild", "Guild")
                        .WithMany("Users")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}