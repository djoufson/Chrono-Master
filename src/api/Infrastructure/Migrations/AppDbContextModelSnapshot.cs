﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Base.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("TeacherId")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalHours")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Definition", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Definitions", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.DefinitionItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

                    b.Property<Guid>("DefinitionId")
                        .HasColumnType("uuid");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone");

                    b.HasKey("Id");

                    b.HasIndex("DefinitionId");

                    b.ToTable("DefinitionItems", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Departments", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Planning", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DefinitionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CourseId")
                        .IsUnique();

                    b.HasIndex("DefinitionId");

                    b.ToTable("Plannings", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.AcademicManager", b =>
                {
                    b.HasBaseType("Domain.Entities.Base.User");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.HasIndex("DepartmentId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("AcademicManager");
                });

            modelBuilder.Entity("Domain.Entities.Admin", b =>
                {
                    b.HasBaseType("Domain.Entities.Base.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Domain.Entities.Teacher", b =>
                {
                    b.HasBaseType("Domain.Entities.Base.User");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("Domain.Entities.Base.User", b =>
                {
                    b.OwnsOne("Domain.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("text");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .HasColumnType("text")
                                .HasColumnName("LastName");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("FullName");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Course", b =>
                {
                    b.HasOne("Domain.Entities.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Department");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Domain.Entities.DefinitionItem", b =>
                {
                    b.HasOne("Domain.Entities.Definition", "Definition")
                        .WithMany("Items")
                        .HasForeignKey("DefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Definition");
                });

            modelBuilder.Entity("Domain.Entities.Planning", b =>
                {
                    b.HasOne("Domain.Entities.Course", "Course")
                        .WithOne("Planning")
                        .HasForeignKey("Domain.Entities.Planning", "CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Definition", "Definition")
                        .WithMany()
                        .HasForeignKey("DefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Domain.Entities.Session", "Sessions", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<TimeSpan>("Duration")
                                .HasColumnType("interval");

                            b1.Property<bool>("PendingUpdates")
                                .HasColumnType("boolean");

                            b1.Property<Guid>("PlanningId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("StartDateTime")
                                .HasColumnType("timestamp with time zone");

                            b1.HasKey("Id");

                            b1.HasIndex("PlanningId");

                            b1.ToTable("Sessions", (string)null);

                            b1.WithOwner("Planning")
                                .HasForeignKey("PlanningId");

                            b1.Navigation("Planning");
                        });

                    b.Navigation("Course");

                    b.Navigation("Definition");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("Domain.Entities.AcademicManager", b =>
                {
                    b.HasOne("Domain.Entities.Department", "Department")
                        .WithOne("Manager")
                        .HasForeignKey("Domain.Entities.AcademicManager", "DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Domain.Entities.Course", b =>
                {
                    b.Navigation("Planning");
                });

            modelBuilder.Entity("Domain.Entities.Definition", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domain.Entities.Department", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Domain.Entities.Teacher", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
