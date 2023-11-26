﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iPractice.DataAccess;

namespace iPractice.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("ClientDtoPsychologistDto", b =>
                {
                    b.Property<long>("ClientsId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PsychologistsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ClientsId", "PsychologistsId");

                    b.HasIndex("PsychologistsId");

                    b.ToTable("ClientDtoPsychologistDto");
                });

            modelBuilder.Entity("iPractice.Application.Contract.Dtos.AvailabilityDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("PsychologistId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PsychologistId");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("iPractice.Application.Contract.Dtos.ClientDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("iPractice.Application.Contract.Dtos.ClientPsychologistDto", b =>
                {
                    b.Property<long>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PsychologistId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ClientId");

                    b.HasIndex("PsychologistId");

                    b.ToTable("ClientsPsychologists");
                });

            modelBuilder.Entity("iPractice.Application.Contract.Dtos.PsychologistDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Psychologists");
                });

            modelBuilder.Entity("iPractice.Application.Contract.Dtos.TimeSlotDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AvailabilityId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTimeFrom")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTimeTo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AvailabilityId");

                    b.ToTable("TimeSlots");
                });

            modelBuilder.Entity("ClientDtoPsychologistDto", b =>
                {
                    b.HasOne("iPractice.Application.Contract.Dtos.ClientDto", null)
                        .WithMany()
                        .HasForeignKey("ClientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iPractice.Application.Contract.Dtos.PsychologistDto", null)
                        .WithMany()
                        .HasForeignKey("PsychologistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("iPractice.Application.Contract.Dtos.AvailabilityDto", b =>
                {
                    b.HasOne("iPractice.Application.Contract.Dtos.PsychologistDto", "Psychologist")
                        .WithMany("Availabilities")
                        .HasForeignKey("PsychologistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Psychologist");
                });

            modelBuilder.Entity("iPractice.Application.Contract.Dtos.ClientPsychologistDto", b =>
                {
                    b.HasOne("iPractice.Application.Contract.Dtos.ClientDto", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iPractice.Application.Contract.Dtos.PsychologistDto", "Psychologist")
                        .WithMany()
                        .HasForeignKey("PsychologistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Psychologist");
                });

            modelBuilder.Entity("iPractice.Application.Contract.Dtos.TimeSlotDto", b =>
                {
                    b.HasOne("iPractice.Application.Contract.Dtos.AvailabilityDto", "Availability")
                        .WithMany("TimeSlots")
                        .HasForeignKey("AvailabilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Availability");
                });

            modelBuilder.Entity("iPractice.Application.Contract.Dtos.AvailabilityDto", b =>
                {
                    b.Navigation("TimeSlots");
                });

            modelBuilder.Entity("iPractice.Application.Contract.Dtos.PsychologistDto", b =>
                {
                    b.Navigation("Availabilities");
                });
#pragma warning restore 612, 618
        }
    }
}
