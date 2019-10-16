﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Tuto.Domain;

namespace Tuto.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tuto.Domain.Models.ChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("RecipientId");

                    b.Property<DateTime>("SendTime");

                    b.Property<Guid>("SenderId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("Tuto.Domain.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Point>("Location");

                    b.Property<string>("Name");

                    b.Property<Guid>("RegionId");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Tuto.Domain.Models.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<DateTime>("LessonTime");

                    b.Property<Guid>("StudentId");

                    b.Property<Guid>("SubjectId");

                    b.Property<Guid>("TeacherId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Tuto.Domain.Models.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Tuto.Domain.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("CreatorId");

                    b.Property<Guid>("ForWhomId");

                    b.Property<int>("Mark");

                    b.Property<string>("Message");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ForWhomId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Tuto.Domain.Models.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Name");

                    b.Property<Guid?>("UserId");

                    b.HasKey("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Tuto.Domain.Models.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Tuto.Domain.Models.TeacherInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<int>("MinimumWage");

                    b.Property<Guid>("SubjectId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("TeacherInfos");
                });

            modelBuilder.Entity("Tuto.Domain.Models.TeacherSubject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("TeacherInfoId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherInfoId");

                    b.ToTable("TeacherSubjects");
                });

            modelBuilder.Entity("Tuto.Domain.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid?>("CityId");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<Point>("Location");

                    b.Property<string>("Name");

                    b.Property<string>("Picture");

                    b.Property<Guid?>("RegionId");

                    b.Property<string>("Surname");

                    b.HasKey("UserId");

                    b.HasIndex("CityId");

                    b.HasIndex("RegionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tuto.Domain.Models.ChatMessage", b =>
                {
                    b.HasOne("Tuto.Domain.Models.User", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tuto.Domain.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tuto.Domain.Models.City", b =>
                {
                    b.HasOne("Tuto.Domain.Models.Region")
                        .WithMany("Cities")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tuto.Domain.Models.Lesson", b =>
                {
                    b.HasOne("Tuto.Domain.Models.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tuto.Domain.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tuto.Domain.Models.User", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tuto.Domain.Models.Review", b =>
                {
                    b.HasOne("Tuto.Domain.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tuto.Domain.Models.User", "ForWhom")
                        .WithMany()
                        .HasForeignKey("ForWhomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tuto.Domain.Models.Role", b =>
                {
                    b.HasOne("Tuto.Domain.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Tuto.Domain.Models.TeacherInfo", b =>
                {
                    b.HasOne("Tuto.Domain.Models.User", "User")
                        .WithOne("TeacherInfo")
                        .HasForeignKey("Tuto.Domain.Models.TeacherInfo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tuto.Domain.Models.TeacherSubject", b =>
                {
                    b.HasOne("Tuto.Domain.Models.TeacherInfo", "TeacherInfo")
                        .WithMany("TeacherSubjects")
                        .HasForeignKey("TeacherInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tuto.Domain.Models.User", b =>
                {
                    b.HasOne("Tuto.Domain.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("Tuto.Domain.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId");
                });
#pragma warning restore 612, 618
        }
    }
}
