﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScrubBot.Database;

namespace ScrubBot.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("ScrubBot.Database.Models.Guild", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 64)));

                    b.Property<string>("AuditChannelId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 64)));

                    b.Property<string>("IconUrl");

                    b.Property<int>("MemberCount");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("Prefix");

                    b.HasKey("Id");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("ScrubBot.Database.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 64)));

                    b.Property<string>("AvatarUrl");

                    b.Property<string>("Discriminator")
                        .HasMaxLength(20);

                    b.Property<string>("GuildId")
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 64)));

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
