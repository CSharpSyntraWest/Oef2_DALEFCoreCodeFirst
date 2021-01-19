using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.DataLayer.Models
{
    public class SchoolDbContext:DbContext
    {
        public SchoolDbContext() { }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            :base(options)
        { 
        
        }
        public virtual DbSet<DbVak> DbVakken { get; set; }
        public virtual DbSet<DbSchool> DbScholen { get; set; }
        public virtual DbSet<DbPersoon> DbPersonen { get; set; }
        public virtual DbSet<DbStudent> DbStudenten { get; set; }
        public virtual DbSet<DbDocent> DbDocenten { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ScholenDb2;Integrated Security=True;Pooling=False");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbSchool>(entity =>
            {
                entity.HasKey(e => e.SchoolId).HasName("PK_School");
                entity.ToTable("School");
                entity.Property(e => e.Naam).HasMaxLength(100).IsUnicode(false);
                entity.HasMany(e => e.Studenten);
                entity.HasMany(e => e.Docenten);
            });
            modelBuilder.Entity<DbPersoon>(entity => {
                entity.HasKey(e => e.PersoonId);//.HasName("PK_Persoon");
                entity.ToTable("Persoon");
                entity.Property(e => e.Voornaam).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Familienaam).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.GeboorteDatum).HasColumnType("datetime").HasDefaultValue(DateTime.Today);
            });
            modelBuilder.Entity<DbStudent>(entity => {
                entity.ToTable("Student");
                entity.HasMany(e => e.Vakken);
            });
            modelBuilder.Entity<DbDocent>(entity => {
                entity.ToTable("Docent");
                entity.Property(e => e.Uurloon).HasColumnType(nameof(System.Decimal)).HasPrecision(2, 2).IsRequired(true);
            });
            modelBuilder.Entity<DbVak>(entity =>
            {
                entity.HasKey(e => e.VakId).HasName("PK_Vak");
                entity.ToTable("Vak");
                entity.Property(e => e.Naam).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.AantalLesuren).HasColumnType("tinyint").HasDefaultValue(0);
                entity.HasMany(e => e.Studenten);
            });
        }
    }
}
