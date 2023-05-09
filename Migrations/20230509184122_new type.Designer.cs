﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tuto.Data;

#nullable disable

namespace tuto.Migrations
{
    [DbContext(typeof(tutoContext))]
    [Migration("20230509184122_new type")]
    partial class newtype
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("tuto.Models.Chambre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdTypeChambre")
                        .HasColumnType("int");

                    b.Property<int>("NumeroChambre")
                        .HasColumnType("int");

                    b.Property<float>("Prix")
                        .HasColumnType("real");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdTypeChambre");

                    b.ToTable("Chambre");
                });

            modelBuilder.Entity("tuto.Models.Facture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateFacture")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<float>("Montant")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("IdClient")
                        .IsUnique();

                    b.ToTable("Facture");
                });

            modelBuilder.Entity("tuto.Models.LoginViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LoginViewModel");

                    b.HasDiscriminator<string>("Discriminator").HasValue("LoginViewModel");
                });

            modelBuilder.Entity("tuto.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateArrivee")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateDepart")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdChambre")
                        .HasColumnType("int");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("NbrChambres")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdChambre");

                    b.HasIndex("IdClient");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("tuto.Models.TypeChambre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeChambre");
                });

            modelBuilder.Entity("tuto.Models.Admin", b =>
                {
                    b.HasBaseType("tuto.Models.LoginViewModel");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("tuto.Models.Client", b =>
                {
                    b.HasBaseType("tuto.Models.LoginViewModel");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("tuto.Models.Chambre", b =>
                {
                    b.HasOne("tuto.Models.TypeChambre", "TypeChambre")
                        .WithMany("Chambres")
                        .HasForeignKey("IdTypeChambre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeChambre");
                });

            modelBuilder.Entity("tuto.Models.Facture", b =>
                {
                    b.HasOne("tuto.Models.Client", "Client")
                        .WithOne("Facture")
                        .HasForeignKey("tuto.Models.Facture", "IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("tuto.Models.Reservation", b =>
                {
                    b.HasOne("tuto.Models.Chambre", "Chambre")
                        .WithMany("Reservations")
                        .HasForeignKey("IdChambre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tuto.Models.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chambre");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("tuto.Models.Chambre", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("tuto.Models.TypeChambre", b =>
                {
                    b.Navigation("Chambres");
                });

            modelBuilder.Entity("tuto.Models.Client", b =>
                {
                    b.Navigation("Facture");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
