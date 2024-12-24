using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudentSystemMvcCore.Models;

namespace StudentSystemMvcCore.DBModel;

public partial class GitstudentContext : DbContext
{
    public GitstudentContext()
    {
    }

    public GitstudentContext(DbContextOptions<GitstudentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<StudentPersonal> StudentPersonals { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:StuConStr");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentPersonal>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.StudentCode });

            entity.ToTable("Student_Personal");

            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nic)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NIC");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
