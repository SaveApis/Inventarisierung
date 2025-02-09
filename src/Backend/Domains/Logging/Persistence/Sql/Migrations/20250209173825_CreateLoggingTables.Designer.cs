﻿// <auto-generated />
using System;
using Backend.Domains.Logging.Persistence.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Domains.Logging.Persistence.Sql.Migrations
{
    [DbContext(typeof(LoggingDbContext))]
    [Migration("20250209173825_CreateLoggingTables")]
    partial class CreateLoggingTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Logging")
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Backend.Domains.Logging.Domain.Entities.LogEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AffectedEntityId")
                        .HasColumnType("char(36)");

                    b.Property<string>("AffectedEntityName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LogType")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("LoggedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("LoggedBy")
                        .HasColumnType("char(36)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AffectedEntityId");

                    b.HasIndex("LogType");

                    b.HasIndex("State");

                    b.ToTable("Entries", "Logging");
                });

            modelBuilder.Entity("Backend.Domains.Logging.Domain.Entities.LogEntryValue", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("AttributeName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("LogEntryId")
                        .HasColumnType("char(36)");

                    b.Property<string>("NewValue")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("OldValue")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("LogEntryId", "AttributeName")
                        .IsUnique();

                    b.ToTable("Values", "Logging");
                });

            modelBuilder.Entity("Backend.Domains.Logging.Domain.Entities.LogEntryValue", b =>
                {
                    b.HasOne("Backend.Domains.Logging.Domain.Entities.LogEntry", null)
                        .WithMany("Values")
                        .HasForeignKey("LogEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Domains.Logging.Domain.Entities.LogEntry", b =>
                {
                    b.Navigation("Values");
                });
#pragma warning restore 612, 618
        }
    }
}
