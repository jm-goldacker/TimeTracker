﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyTime.Models.Database;

#nullable disable

namespace MyTime.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220112205354_AddedTaskTimes")]
    partial class AddedTaskTimes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("MyTime.Models.Database.PauseTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<int>("WorkTimeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("WorkTimeId");

                    b.ToTable("PauseTimes");
                });

            modelBuilder.Entity("MyTime.Models.Database.TaskTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TaskTimes");
                });

            modelBuilder.Entity("MyTime.Models.Database.WorkTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WorkTimes");
                });

            modelBuilder.Entity("MyTime.Models.Database.PauseTime", b =>
                {
                    b.HasOne("MyTime.Models.Database.WorkTime", "WorkTime")
                        .WithMany("PauseTimes")
                        .HasForeignKey("WorkTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkTime");
                });

            modelBuilder.Entity("MyTime.Models.Database.WorkTime", b =>
                {
                    b.Navigation("PauseTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
