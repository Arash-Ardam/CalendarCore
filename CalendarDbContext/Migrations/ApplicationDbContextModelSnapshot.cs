﻿// <auto-generated />
using System;
using CalendarDbContext.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CalendarDbContext.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CalendarDomain.Calendar", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Weekend")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("CalendarDomain.DateEvent", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CalendarName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsHoliday")
                        .HasColumnType("bit");

                    b.HasKey("Date", "Description");

                    b.HasIndex("CalendarName");

                    b.ToTable("DateEvent");
                });

            modelBuilder.Entity("CalendarDomain.DateEvent", b =>
                {
                    b.HasOne("CalendarDomain.Calendar", null)
                        .WithMany("Events")
                        .HasForeignKey("CalendarName");
                });

            modelBuilder.Entity("CalendarDomain.Calendar", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
