﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Doska.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Doska.Migrations.Migrations
{
    [DbContext(typeof(MigrationsDbContext))]
    [Migration("20230326111312_Update")]
    partial class Update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Doska.Domain.Ad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("character varying(800)");

                    b.Property<Guid?>("FavoriteAdId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("SubcategoryId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FavoriteAdId");

                    b.HasIndex("OrderId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Ad");
                });

            modelBuilder.Entity("Doska.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Doska.Domain.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InitializerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("InitializerId");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("Doska.Domain.FavoriteAd", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("FavoriteAd");
                });

            modelBuilder.Entity("Doska.Domain.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid");

                    b.Property<string>("Containment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Doska.Domain.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<List<Guid>>("ProductsIds")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Doska.Domain.Subcategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategory");
                });

            modelBuilder.Entity("Doska.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Doska.Domain.Ad", b =>
                {
                    b.HasOne("Doska.Domain.FavoriteAd", null)
                        .WithMany("Ad")
                        .HasForeignKey("FavoriteAdId");

                    b.HasOne("Doska.Domain.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.HasOne("Doska.Domain.Subcategory", "Subcategory")
                        .WithMany()
                        .HasForeignKey("SubcategoryId");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("Doska.Domain.Chat", b =>
                {
                    b.HasOne("Doska.Domain.User", "Initializer")
                        .WithMany()
                        .HasForeignKey("InitializerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Initializer");
                });

            modelBuilder.Entity("Doska.Domain.FavoriteAd", b =>
                {
                    b.HasOne("Doska.Domain.User", null)
                        .WithOne("Cart")
                        .HasForeignKey("Doska.Domain.FavoriteAd", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Doska.Domain.Message", b =>
                {
                    b.HasOne("Doska.Domain.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doska.Domain.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Doska.Domain.Order", b =>
                {
                    b.HasOne("Doska.Domain.User", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Doska.Domain.Subcategory", b =>
                {
                    b.HasOne("Doska.Domain.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Doska.Domain.Category", b =>
                {
                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("Doska.Domain.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Doska.Domain.FavoriteAd", b =>
                {
                    b.Navigation("Ad");
                });

            modelBuilder.Entity("Doska.Domain.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Doska.Domain.User", b =>
                {
                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
