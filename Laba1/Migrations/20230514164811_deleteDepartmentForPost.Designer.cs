﻿// <auto-generated />
using System;
using Laba1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Laba1.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230514164811_deleteDepartmentForPost")]
    partial class deleteDepartmentForPost
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Laba1.Models.AdressDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("House")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdressDepartments");
                });

            modelBuilder.Entity("Laba1.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("idAdressDepartment")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("idAdressDepartment");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Laba1.Models.DepartmentsAndPostsOfWorker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int?>("DescriptionsWorkerId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DescriptionsWorkerId");

                    b.HasIndex("PostId");

                    b.HasIndex("WorkerId");

                    b.ToTable("DepartmentsAndPostsOfWorker");
                });

            modelBuilder.Entity("Laba1.Models.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DescriptionsWorkerId")
                        .HasColumnType("int");

                    b.Property<int?>("WorkerId")
                        .HasColumnType("int");

                    b.Property<string>("diplomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("diplomSeries")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("special")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("yearEnd")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DescriptionsWorkerId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("Laba1.Models.LaborBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DescriptionsWorkerId")
                        .HasColumnType("int");

                    b.Property<int?>("WorkerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateRecord")
                        .HasColumnType("datetime2");

                    b.Property<string>("intelligenceWork")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nameDocument")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nameWork")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("numberDocument")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DescriptionsWorkerId");

                    b.HasIndex("WorkerId");

                    b.ToTable("LaborBook");
                });

            modelBuilder.Entity("Laba1.Models.MedicalBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DescriptionsWorkerId")
                        .HasColumnType("int");

                    b.Property<int?>("WorkerId")
                        .HasColumnType("int");

                    b.Property<string>("conclusion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateExam")
                        .HasColumnType("datetime2");

                    b.Property<string>("placeExam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DescriptionsWorkerId");

                    b.HasIndex("WorkerId");

                    b.ToTable("MedicalBook");
                });

            modelBuilder.Entity("Laba1.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Laba1.Models.Vacation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DescriptionsWorkerId")
                        .HasColumnType("int");

                    b.Property<int?>("WorkerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("typeVacation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DescriptionsWorkerId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Vacations");
                });

            modelBuilder.Entity("Laba1.Models.ViewModels.DescriptionsWorker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("workerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("workerId");

                    b.ToTable("DescriptionsWorker");
                });

            modelBuilder.Entity("Laba1.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Children")
                        .HasColumnType("int");

                    b.Property<string>("CityHabitation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescriptionWorker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DivisionCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseHabitation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuedByWhom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Middlename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberInn")
                        .HasColumnType("int");

                    b.Property<int>("NumberPass")
                        .HasColumnType("int");

                    b.Property<int>("NumberSnils")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeriesPass")
                        .HasColumnType("int");

                    b.Property<string>("StreetHabitation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("dissmisStatus")
                        .HasColumnType("bit");

                    b.Property<string>("military_title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name_kommis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("profile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("shelf_life")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("stock_category")
                        .HasColumnType("int");

                    b.Property<string>("vus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("Laba1.Models.Department", b =>
                {
                    b.HasOne("Laba1.Models.AdressDepartment", "AdressDepartment")
                        .WithMany()
                        .HasForeignKey("idAdressDepartment")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdressDepartment");
                });

            modelBuilder.Entity("Laba1.Models.DepartmentsAndPostsOfWorker", b =>
                {
                    b.HasOne("Laba1.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Laba1.Models.ViewModels.DescriptionsWorker", null)
                        .WithMany("departmentsAndPostsOfWorkers")
                        .HasForeignKey("DescriptionsWorkerId");

                    b.HasOne("Laba1.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Laba1.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Post");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Laba1.Models.Education", b =>
                {
                    b.HasOne("Laba1.Models.ViewModels.DescriptionsWorker", null)
                        .WithMany("educations")
                        .HasForeignKey("DescriptionsWorkerId");

                    b.HasOne("Laba1.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Laba1.Models.LaborBook", b =>
                {
                    b.HasOne("Laba1.Models.ViewModels.DescriptionsWorker", null)
                        .WithMany("laborBooks")
                        .HasForeignKey("DescriptionsWorkerId");

                    b.HasOne("Laba1.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Laba1.Models.MedicalBook", b =>
                {
                    b.HasOne("Laba1.Models.ViewModels.DescriptionsWorker", null)
                        .WithMany("medicalBooks")
                        .HasForeignKey("DescriptionsWorkerId");

                    b.HasOne("Laba1.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Laba1.Models.Vacation", b =>
                {
                    b.HasOne("Laba1.Models.ViewModels.DescriptionsWorker", null)
                        .WithMany("vacations")
                        .HasForeignKey("DescriptionsWorkerId");

                    b.HasOne("Laba1.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Laba1.Models.ViewModels.DescriptionsWorker", b =>
                {
                    b.HasOne("Laba1.Models.Worker", "worker")
                        .WithMany()
                        .HasForeignKey("workerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("worker");
                });

            modelBuilder.Entity("Laba1.Models.ViewModels.DescriptionsWorker", b =>
                {
                    b.Navigation("departmentsAndPostsOfWorkers");

                    b.Navigation("educations");

                    b.Navigation("laborBooks");

                    b.Navigation("medicalBooks");

                    b.Navigation("vacations");
                });
#pragma warning restore 612, 618
        }
    }
}
