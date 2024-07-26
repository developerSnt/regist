﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using regist;

#nullable disable

namespace regist.Migrations
{
    [DbContext(typeof(AppData))]
    [Migration("20240720072315_newslistmg2")]
    partial class newslistmg2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("regist.DataInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Domion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("elementClass")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("elementId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("source")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DataInfos");
                });

            modelBuilder.Entity("regist.Regis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("certificate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("comformpassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Regist");
                });
#pragma warning restore 612, 618
        }
    }
}
