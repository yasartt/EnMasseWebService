﻿// <auto-generated />
using System;
using EnMasseWebService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnMasseWebService.Migrations
{
    [DbContext(typeof(EnteractDbContext))]
    partial class EnteractDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EnMasseWebService.Models.Entities.Cafe", b =>
                {
                    b.Property<int>("CafeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CafeId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CafeId");

                    b.ToTable("Cafes");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.CafeMessage", b =>
                {
                    b.Property<int>("CafeMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CafeMessageId"), 1L, 1);

                    b.Property<int>("CafeId")
                        .HasColumnType("int");

                    b.Property<string>("CafeMessageText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SenderUserId")
                        .HasColumnType("int");

                    b.HasKey("CafeMessageId");

                    b.HasIndex("CafeId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("CafeMessages");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.CafeUser", b =>
                {
                    b.Property<int>("CafeUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CafeUserId"), 1L, 1);

                    b.Property<int>("CafeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CafeUserId");

                    b.HasIndex("CafeId");

                    b.HasIndex("UserId");

                    b.ToTable("CafeUsers");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.Daily", b =>
                {
                    b.Property<int>("DailyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DailyId"), 1L, 1);

                    b.Property<string>("Caption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DailyId");

                    b.HasIndex("UserId");

                    b.ToTable("Dailies");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.DailyImage", b =>
                {
                    b.Property<int>("DailyImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DailyImageId"), 1L, 1);

                    b.Property<int>("DailyId")
                        .HasColumnType("int");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DailyImageId");

                    b.HasIndex("DailyId");

                    b.ToTable("DailyImages");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.UserContacts", b =>
                {
                    b.Property<int>("UserContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserContactId"), 1L, 1);

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserContactId");

                    b.HasIndex("ContactId");

                    b.HasIndex("UserId");

                    b.ToTable("UserContacts");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.CafeMessage", b =>
                {
                    b.HasOne("EnMasseWebService.Models.Entities.Cafe", "Cafe")
                        .WithMany()
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnMasseWebService.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cafe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.CafeUser", b =>
                {
                    b.HasOne("EnMasseWebService.Models.Entities.Cafe", "cafe")
                        .WithMany()
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnMasseWebService.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("cafe");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.Daily", b =>
                {
                    b.HasOne("EnMasseWebService.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.DailyImage", b =>
                {
                    b.HasOne("EnMasseWebService.Models.Entities.Daily", "Daily")
                        .WithMany()
                        .HasForeignKey("DailyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Daily");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.UserContacts", b =>
                {
                    b.HasOne("EnMasseWebService.Models.Entities.User", "Contact")
                        .WithMany("ContactUsers")
                        .HasForeignKey("ContactId")
                        .IsRequired();

                    b.HasOne("EnMasseWebService.Models.Entities.User", "User")
                        .WithMany("UserContacts")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Contact");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnMasseWebService.Models.Entities.User", b =>
                {
                    b.Navigation("ContactUsers");

                    b.Navigation("UserContacts");
                });
#pragma warning restore 612, 618
        }
    }
}
