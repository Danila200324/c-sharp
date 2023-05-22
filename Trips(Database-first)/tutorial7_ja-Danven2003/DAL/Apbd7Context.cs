﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Zadanie7.DAL;

public partial class Apbd7Context : DbContext
{
    public Apbd7Context()
    {
    }

    public Apbd7Context(DbContextOptions<Apbd7Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientTrip> ClientTrips { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PRIMARY");

            entity.ToTable("client");

            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.Email).HasMaxLength(120);
            entity.Property(e => e.FirstName).HasMaxLength(120);
            entity.Property(e => e.LastName).HasMaxLength(120);
            entity.Property(e => e.Pesel).HasMaxLength(120);
            entity.Property(e => e.Telephone).HasMaxLength(120);
        });

        modelBuilder.Entity<ClientTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdTrip }).HasName("PRIMARY");

            entity.ToTable("client_trip");

            entity.HasIndex(e => e.IdTrip, "Client_Trip_Trip");

            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.IdTrip).HasColumnName("idTrip");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.RegisteredAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Client_Trip_Client");

            entity.HasOne(d => d.IdTripNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdTrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Client_Trip_Trip");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("PRIMARY");

            entity.ToTable("country");

            entity.Property(e => e.IdCountry).HasColumnName("idCountry");
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip).HasName("PRIMARY");

            entity.ToTable("trip");

            entity.Property(e => e.IdTrip).HasColumnName("idTrip");
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(120);
            entity.Property(e => e.Name).HasMaxLength(120);

            entity.HasMany(d => d.Countries).WithMany(p => p.IdTrips)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryTrip",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("IdCountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Country"),
                    l => l.HasOne<Trip>().WithMany()
                        .HasForeignKey("IdTrip")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Trip"),
                    j =>
                    {
                        j.HasKey("IdTrip", "IdCountry").HasName("PRIMARY");
                        j.ToTable("country_trip");
                        j.HasIndex(new[] { "IdCountry" }, "Country_Trip_Country");
                        j.IndexerProperty<int>("IdTrip").HasColumnName("idTrip");
                        j.IndexerProperty<int>("IdCountry").HasColumnName("idCountry");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
