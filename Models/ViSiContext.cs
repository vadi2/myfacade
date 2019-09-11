using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace myfacade.Models
{
    public partial class ViSiContext : DbContext
    {
        public ViSiContext()
        {
        }

        public ViSiContext(DbContextOptions<ViSiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BloodPressure> BloodPressure { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("MultipleActiveResultSets=true;Server=tcp:.;User ID=SA;Password=Kz6ai9wp9mrHsx9rWmjy;Connect Timeout=5;Integrated Security=false;Persist Security Info=False;Initial Catalog=ViSi;Data Source=localhost");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<BloodPressure>(entity =>
            {
                entity.Property(e => e.MeasuredAt).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.BloodPressure)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BloodPres__Patie__38996AB5");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FamilyName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PatientNumber)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
