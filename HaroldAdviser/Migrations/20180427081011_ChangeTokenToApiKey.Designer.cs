﻿// <auto-generated />
using HaroldAdviser.Data;
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
    [Migration("20180427081011_ChangeTokenToApiKey")]
    partial class ChangeTokenToApiKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("HaroldAdviser.Data.Repository", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApiKey");

                    b.Property<bool>("Checked");

                    b.Property<string>("Url");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Repositories");
                });

            modelBuilder.Entity("HaroldAdviser.Data.Warning", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("File");

                    b.Property<string>("Kind");

                    b.Property<string>("Lines");

                    b.Property<string>("Message");

                    b.Property<Guid?>("RepositoryId");

                    b.HasKey("Id");

                    b.HasIndex("RepositoryId");

                    b.ToTable("Warnings");
                });

            modelBuilder.Entity("HaroldAdviser.Data.Warning", b =>
                {
                    b.HasOne("HaroldAdviser.Data.Repository")
                        .WithMany("Warnings")
                        .HasForeignKey("RepositoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
