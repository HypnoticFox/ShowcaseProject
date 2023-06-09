﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShowcaseProject.Products.Infrastructure;

#nullable disable

namespace ShowcaseProject.Products.Infrastructure.Migrations
{
    [DbContext(typeof(ProductsContext))]
    [Migration("20230710144245_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AmountInStock")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("_productStatusId")
                        .HasColumnType("int")
                        .HasColumnName("ProductStatusId");

                    b.HasKey("Id");

                    b.HasIndex("_productStatusId");

                    b.ToTable("products", "products");
                });

            modelBuilder.Entity("ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate.ProductStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("productstatus", "products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "concept",
                            Name = "Concept"
                        },
                        new
                        {
                            Id = 2,
                            Code = "available",
                            Name = "Available"
                        },
                        new
                        {
                            Id = 3,
                            Code = "unavailable",
                            Name = "Unavailable"
                        },
                        new
                        {
                            Id = 4,
                            Code = "discontinued",
                            Name = "Discontinued"
                        });
                });

            modelBuilder.Entity("ShowcaseProject.Products.Infrastructure.Idempotency.ClientRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("requests", "products");
                });

            modelBuilder.Entity("ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate.Product", b =>
                {
                    b.HasOne("ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate.ProductStatus", null)
                        .WithMany()
                        .HasForeignKey("_productStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
