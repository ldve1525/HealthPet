using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HealthPet.Models
{
    public partial class HealthPetDBContext : DbContext
    {
        public HealthPetDBContext()
        {
        }

        public HealthPetDBContext(DbContextOptions<HealthPetDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.Categorie)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Shift)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdCard)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PetName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PetType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Race).HasMaxLength(50);
            });
        }
    }
}
