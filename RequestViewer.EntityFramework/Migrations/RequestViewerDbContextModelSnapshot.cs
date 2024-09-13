﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RequestViewer.EntityFramework.Migrations
{
    [DbContext(typeof(RequestViewerDbContext))]
    partial class RequestViewerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("RequestViewer.EntityFramework.DTOs.PeriodDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Periods");
                });

            modelBuilder.Entity("RequestViewer.EntityFramework.DTOs.RequestDayDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AllowedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("RequestId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestDays");
                });

            modelBuilder.Entity("RequestViewer.EntityFramework.DTOs.RequestDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PeriodId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PeriodId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("RequestViewer.EntityFramework.DTOs.UserDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActiveDirectoryCN")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RequestViewer.EntityFramework.DTOs.RequestDayDto", b =>
                {
                    b.HasOne("RequestViewer.EntityFramework.DTOs.RequestDto", "Request")
                        .WithMany("RequestsDays")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("RequestViewer.EntityFramework.DTOs.RequestDto", b =>
                {
                    b.HasOne("RequestViewer.EntityFramework.DTOs.PeriodDto", "Period")
                        .WithMany("Requests")
                        .HasForeignKey("PeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Period");
                });

            modelBuilder.Entity("RequestViewer.EntityFramework.DTOs.PeriodDto", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("RequestViewer.EntityFramework.DTOs.RequestDto", b =>
                {
                    b.Navigation("RequestsDays");
                });
#pragma warning restore 612, 618
        }
    }
}
