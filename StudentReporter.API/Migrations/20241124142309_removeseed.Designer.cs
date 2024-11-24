﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentReporter.API.Data;

#nullable disable

namespace StudentReporter.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241124142309_removeseed")]
    partial class removeseed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentReporter.API.Model.AttendanceSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TotalAttendances")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("AttendanceSummaries");
                });

            modelBuilder.Entity("StudentReporter.API.Model.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Attended"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Absent"
                        });
                });

            modelBuilder.Entity("StudentReporter.API.Model.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentReporter.API.Model.StudentSubject", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("StudentReporter.API.Model.StudentWeekDay", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("WeekDayId")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "WeekDayId");

                    b.HasIndex("StatusId");

                    b.HasIndex("WeekDayId");

                    b.ToTable("StudentWeekDays");
                });

            modelBuilder.Entity("StudentReporter.API.Model.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("StudentReporter.API.Model.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("StudentReporter.API.Model.WeekDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Week")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WeekDays");
                });

            modelBuilder.Entity("StudentReporter.API.Model.AttendanceSummary", b =>
                {
                    b.HasOne("StudentReporter.API.Model.Student", "Student")
                        .WithMany("AttendanceSummaries")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentReporter.API.Model.Subject", "Subject")
                        .WithMany("AttendanceSummaries")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("StudentReporter.API.Model.StudentSubject", b =>
                {
                    b.HasOne("StudentReporter.API.Model.Student", "Student")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentReporter.API.Model.Subject", "Subject")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("StudentReporter.API.Model.StudentWeekDay", b =>
                {
                    b.HasOne("StudentReporter.API.Model.Status", "Status")
                        .WithMany("StudentWeekDays")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("StudentReporter.API.Model.Student", "Student")
                        .WithMany("StudentWeekDay")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentReporter.API.Model.WeekDay", "WeekDay")
                        .WithMany("StudentWeekDay")
                        .HasForeignKey("WeekDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("Student");

                    b.Navigation("WeekDay");
                });

            modelBuilder.Entity("StudentReporter.API.Model.Subject", b =>
                {
                    b.HasOne("StudentReporter.API.Model.Teacher", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("StudentReporter.API.Model.Status", b =>
                {
                    b.Navigation("StudentWeekDays");
                });

            modelBuilder.Entity("StudentReporter.API.Model.Student", b =>
                {
                    b.Navigation("AttendanceSummaries");

                    b.Navigation("StudentSubjects");

                    b.Navigation("StudentWeekDay");
                });

            modelBuilder.Entity("StudentReporter.API.Model.Subject", b =>
                {
                    b.Navigation("AttendanceSummaries");

                    b.Navigation("StudentSubjects");
                });

            modelBuilder.Entity("StudentReporter.API.Model.Teacher", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("StudentReporter.API.Model.WeekDay", b =>
                {
                    b.Navigation("StudentWeekDay");
                });
#pragma warning restore 612, 618
        }
    }
}