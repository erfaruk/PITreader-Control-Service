﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReaderWorker.Models.Database;

public partial class ReaderExpertContext : DbContext
{
    public ReaderExpertContext()
    {
    }

    public ReaderExpertContext(DbContextOptions<ReaderExpertContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pitreader> Pitreaders { get; set; }

    public virtual DbSet<Transponder> Transponders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BNACLH9;Database=ReaderExpert;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pitreader>(entity =>
        {
            entity.HasKey(e => e.ReaderId);

            entity.ToTable("PITreaders");

            entity.Property(e => e.Apitoken)
                .HasMaxLength(30)
                .HasColumnName("APIToken");
            entity.Property(e => e.Fingerprint).HasMaxLength(60);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(12)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Location).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Transponder>(entity =>
        {
            entity.HasKey(e => e.SecurityId);

            entity.Property(e => e.SecurityId).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.SerialNo).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Transponders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Transponders_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber).HasMaxLength(12);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}