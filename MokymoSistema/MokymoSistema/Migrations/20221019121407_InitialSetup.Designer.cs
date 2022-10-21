﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MokymoSistema.Models;

#nullable disable

namespace MokymoSistema.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221019121407_InitialSetup")]
    partial class InitialSetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CourseUser", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("CoursesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("CourseUser");
                });

            modelBuilder.Entity("MokymoSistema.Models.AssignmentUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GradeId")
                        .HasColumnType("int");

                    b.Property<int?>("LectureId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("UploadUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GradeId");

                    b.HasIndex("LectureId");

                    b.HasIndex("StudentId");

                    b.ToTable("AssignmentUploads");
                });

            modelBuilder.Entity("MokymoSistema.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("MokymoSistema.Models.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Result")
                        .HasColumnType("float");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("MokymoSistema.Models.Lecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AssignmentFileUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("LecturerId")
                        .HasColumnType("int");

                    b.Property<string>("MaterialFileUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("LecturerId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("MokymoSistema.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CourseUser", b =>
                {
                    b.HasOne("MokymoSistema.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MokymoSistema.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MokymoSistema.Models.AssignmentUpload", b =>
                {
                    b.HasOne("MokymoSistema.Models.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId");

                    b.HasOne("MokymoSistema.Models.Lecture", null)
                        .WithMany("AssignmentUploads")
                        .HasForeignKey("LectureId");

                    b.HasOne("MokymoSistema.Models.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.Navigation("Grade");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MokymoSistema.Models.Grade", b =>
                {
                    b.HasOne("MokymoSistema.Models.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MokymoSistema.Models.Lecture", b =>
                {
                    b.HasOne("MokymoSistema.Models.Course", null)
                        .WithMany("Lectures")
                        .HasForeignKey("CourseId");

                    b.HasOne("MokymoSistema.Models.User", "Lecturer")
                        .WithMany()
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecturer");
                });

            modelBuilder.Entity("MokymoSistema.Models.Course", b =>
                {
                    b.Navigation("Lectures");
                });

            modelBuilder.Entity("MokymoSistema.Models.Lecture", b =>
                {
                    b.Navigation("AssignmentUploads");
                });
#pragma warning restore 612, 618
        }
    }
}