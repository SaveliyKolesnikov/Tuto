﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tuto.Domain;

namespace Tuto.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191013143426_Add-review-model-to-context")]
    partial class Addreviewmodeltocontext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tuto.Domain.Models.ChatMessage", b =>
                {
                    b.Property<Guid>("ChatMessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("RecipientId");

                    b.Property<DateTime>("SendTime");

                    b.Property<Guid>("SenderId");

                    b.Property<string>("Text");

                    b.HasKey("ChatMessageId");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("Tuto.Domain.Models.Lesson", b =>
                {
                    b.Property<Guid>("LessonId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LessonTime");

                    b.Property<Guid>("StudentId");

                    b.Property<Guid>("TeacherId");

                    b.Property<string>("Text");

                    b.HasKey("LessonId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Tuto.Domain.Models.Review", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatorId");

                    b.Property<Guid>("ForWhomId");

                    b.Property<int>("Mark");

                    b.Property<string>("Message");

                    b.HasKey("ReviewId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ForWhomId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Tuto.Domain.Models.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid?>("UserId");

                    b.HasKey("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Tuto.Domain.Models.TeacherInfo", b =>
                {
                    b.Property<Guid>("TeacherInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid>("UserId");

                    b.HasKey("TeacherInfoId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("TeacherInfos");
                });

            modelBuilder.Entity("Tuto.Domain.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email");

                    b.HasKey("UserId");

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

            modelBuilder.Entity("Tuto.Domain.Models.Lesson", b =>
                {
                    b.HasOne("Tuto.Domain.Models.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
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
#pragma warning restore 612, 618
        }
    }
}
