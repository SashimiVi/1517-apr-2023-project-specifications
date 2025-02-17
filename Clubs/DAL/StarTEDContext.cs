﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#region Additional Namespaces
using Clubs.Entities;
#endregion

namespace Clubs.DAL
{
    public partial class StarTEDContext : DbContext
    {
        public StarTEDContext()
        {
        }

        public StarTEDContext(DbContextOptions<StarTEDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Club> Clubs { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_dbo.Clubs_dbo.Employees_EmployeeID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}