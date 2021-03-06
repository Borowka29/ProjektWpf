﻿// <auto-generated />
using System;
using ListaZadan.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ListaZadan.Migrations
{
    [DbContext(typeof(ListaZadanContext))]
    [Migration("20210120224627_update1")]
    partial class update1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ListaZadan.Models.Kategora_Zadanie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("KategoriaIdKategoria")
                        .HasColumnType("int");

                    b.Property<int>("ZadanieIdZadanie")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KategoriaIdKategoria");

                    b.HasIndex("ZadanieIdZadanie");

                    b.ToTable("Kategoria_Zadanie");
                });

            modelBuilder.Entity("ListaZadan.Models.Kategoria", b =>
                {
                    b.Property<int>("IdKategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Typ")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdKategoria");

                    b.ToTable("Kategorie");
                });

            modelBuilder.Entity("ListaZadan.Models.Podzadania", b =>
                {
                    b.Property<int>("IdPodzadania")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("ZadanieIdZadanie")
                        .HasColumnType("int");

                    b.Property<int>("któreNaLiscie")
                        .HasColumnType("int");

                    b.Property<string>("opis")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPodzadania");

                    b.HasIndex("ZadanieIdZadanie");

                    b.ToTable("Podzadania");
                });

            modelBuilder.Entity("ListaZadan.Models.Zadanie", b =>
                {
                    b.Property<int>("IdZadanie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Tresc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("prorytet")
                        .HasColumnType("float");

                    b.Property<DateTime?>("rozpoczecie")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("zakonczenie")
                        .HasColumnType("datetime2");

                    b.HasKey("IdZadanie");

                    b.ToTable("Zadania");
                });

            modelBuilder.Entity("ListaZadan.Models.Kategora_Zadanie", b =>
                {
                    b.HasOne("ListaZadan.Models.Kategoria", "Kategoria")
                        .WithMany("Kategora_Zadanie")
                        .HasForeignKey("KategoriaIdKategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ListaZadan.Models.Zadanie", "Zadanie")
                        .WithMany("Kategora_Zadanie")
                        .HasForeignKey("ZadanieIdZadanie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategoria");

                    b.Navigation("Zadanie");
                });

            modelBuilder.Entity("ListaZadan.Models.Podzadania", b =>
                {
                    b.HasOne("ListaZadan.Models.Zadanie", "Zadanie")
                        .WithMany("Podzadania")
                        .HasForeignKey("ZadanieIdZadanie");

                    b.Navigation("Zadanie");
                });

            modelBuilder.Entity("ListaZadan.Models.Kategoria", b =>
                {
                    b.Navigation("Kategora_Zadanie");
                });

            modelBuilder.Entity("ListaZadan.Models.Zadanie", b =>
                {
                    b.Navigation("Kategora_Zadanie");

                    b.Navigation("Podzadania");
                });
#pragma warning restore 612, 618
        }
    }
}
