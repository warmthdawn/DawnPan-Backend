﻿// <auto-generated />
using System;
using DawnPan.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DawnPan.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211012185213_0.2")]
    partial class _02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("DawnPan.Entity.FileDirectory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Directories");
                });

            modelBuilder.Entity("DawnPan.Entity.FileItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("DirectoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("FileName")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Hash")
                        .HasColumnType("blob(384)");

                    b.Property<long>("PropertyId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DirectoryId");

                    b.HasIndex("PropertyId")
                        .IsUnique();

                    b.ToTable("Files");
                });

            modelBuilder.Entity("DawnPan.Entity.FileProperty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("FileProperties");
                });

            modelBuilder.Entity("DawnPan.Entity.FileDirectory", b =>
                {
                    b.HasOne("DawnPan.Entity.FileDirectory", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("DawnPan.Entity.FileItem", b =>
                {
                    b.HasOne("DawnPan.Entity.FileDirectory", "Directory")
                        .WithMany("Files")
                        .HasForeignKey("DirectoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DawnPan.Entity.FileProperty", "Property")
                        .WithOne()
                        .HasForeignKey("DawnPan.Entity.FileItem", "PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Directory");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("DawnPan.Entity.FileDirectory", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}