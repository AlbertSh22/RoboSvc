using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EntityDal.Models;

namespace EntityDal.Context
{
    public partial class RoboSvcContext : DbContext
    {
        public RoboSvcContext()
        {
        }

        public RoboSvcContext(DbContextOptions<RoboSvcContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Language> Languages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=RoboSvc;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Languages", "clf");

                entity.HasComment("Classifier Languages");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.Alpha2)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Codes for the representation of names of languages—Part 1: Alpha-2 code");

                entity.Property(e => e.DigitalCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("The digital code consisting of 3 Arabic numerals and assigned to languages arranged in the order of Russian names.");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasComment("ISO language name");

                entity.Property(e => e.Notes).HasComment("Language notes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
