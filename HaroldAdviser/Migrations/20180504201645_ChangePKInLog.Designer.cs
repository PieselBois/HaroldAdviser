﻿// <auto-generated />
using HaroldAdviser.Data;
using HaroldAdviser.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HaroldAdviser.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180504201645_ChangePKInLog")]
    partial class ChangePKInLog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("HaroldAdviser.Data.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Module");

                    b.Property<Guid?>("PipelineId");

                    b.Property<int>("Type");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("PipelineId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("HaroldAdviser.Data.Pipeline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CloneUrl");

                    b.Property<Guid?>("RepositoryId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("RepositoryId");

                    b.ToTable("Pipelines");
                });

            modelBuilder.Entity("HaroldAdviser.Data.Repository", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Checked");

                    b.Property<string>("Name");

                    b.Property<Guid?>("SettingsId");

                    b.Property<string>("Url");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SettingsId");

                    b.ToTable("Repositories");
                });

            modelBuilder.Entity("HaroldAdviser.Data.RepositorySettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("HaroldAdviser.Data.Warning", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("File");

                    b.Property<string>("Kind");

                    b.Property<string>("Lines");

                    b.Property<string>("Message");

                    b.Property<Guid?>("PipelineId");

                    b.Property<Guid?>("RepositoryId");

                    b.HasKey("Id");

                    b.HasIndex("PipelineId");

                    b.HasIndex("RepositoryId");

                    b.ToTable("Warnings");
                });

            modelBuilder.Entity("HaroldAdviser.Data.Log", b =>
                {
                    b.HasOne("HaroldAdviser.Data.Pipeline")
                        .WithMany("Logs")
                        .HasForeignKey("PipelineId");
                });

            modelBuilder.Entity("HaroldAdviser.Data.Pipeline", b =>
                {
                    b.HasOne("HaroldAdviser.Data.Repository", "Repository")
                        .WithMany("Pipelines")
                        .HasForeignKey("RepositoryId");
                });

            modelBuilder.Entity("HaroldAdviser.Data.Repository", b =>
                {
                    b.HasOne("HaroldAdviser.Data.RepositorySettings", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsId");
                });

            modelBuilder.Entity("HaroldAdviser.Data.Warning", b =>
                {
                    b.HasOne("HaroldAdviser.Data.Pipeline")
                        .WithMany("Warnings")
                        .HasForeignKey("PipelineId");

                    b.HasOne("HaroldAdviser.Data.Repository")
                        .WithMany("Warnings")
                        .HasForeignKey("RepositoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
