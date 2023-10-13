﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDo.Infra.Data.Context;

#nullable disable

namespace ToDo.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ToDo.Domain.Models.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AssignmentListId")
                        .HasColumnType("int")
                        .HasColumnName("assignment_list_id");

                    b.Property<bool>("Concluded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("concluded");

                    b.Property<DateTime?>("ConcludedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("concluded_at");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_at");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("deadline");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("description");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentListId");

                    b.HasIndex("UserId");

                    b.ToTable("assignments", (string)null);
                });

            modelBuilder.Entity("ToDo.Domain.Models.AssignmentList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("assignment_list", (string)null);
                });

            modelBuilder.Entity("ToDo.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("password");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_at");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ToDo.Domain.Models.Assignment", b =>
                {
                    b.HasOne("ToDo.Domain.Models.AssignmentList", "AssignmentList")
                        .WithMany("Assignments")
                        .HasForeignKey("AssignmentListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToDo.Domain.Models.User", "User")
                        .WithMany("Assignments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignmentList");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDo.Domain.Models.AssignmentList", b =>
                {
                    b.HasOne("ToDo.Domain.Models.User", "User")
                        .WithMany("AssignmentLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDo.Domain.Models.AssignmentList", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("ToDo.Domain.Models.User", b =>
                {
                    b.Navigation("AssignmentLists");

                    b.Navigation("Assignments");
                });
#pragma warning restore 612, 618
        }
    }
}
