using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace myfacade.Models
{
    public partial class ViSiContext : DbContext
    {
        private readonly IOptions<DbOptions> _dbOptionsAccessor;

        public ViSiContext(IOptions<DbOptions> dbOptionsAccessor)
        {
            _dbOptionsAccessor = dbOptionsAccessor;
        }

        public virtual DbSet<ViSiBloodPressure> ViSiBloodPressure { get; set; }
        public virtual DbSet<ViSiPatient> ViSiPatient { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // "MultipleActiveResultSets=true;Server=tcp:.;User ID=SA;Password=Kz6ai9wp9mrHsx9rWmjy;Connect Timeout=5;Integrated Security=false;Persist Security Info=False;Initial Catalog=ViSi;Data Source=localhost"
                optionsBuilder.UseSqlServer(_dbOptionsAccessor.Value.ConnectionString);
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
