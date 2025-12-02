using Microsoft.EntityFrameworkCore;
using PatientPortal.Shared;

namespace PatientPortal.API.Data
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options) { }

        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for testing
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, PatientId = 1, DoctorId = 101, AppointmentDate = DateTime.Now.AddDays(2), Reason = "General Checkup", IsConfirmed = true }
            );

            modelBuilder.Entity<MedicalRecord>().HasData(
                new MedicalRecord { Id = 1, PatientId = 1, DoctorId = 101, DateCreated = DateTime.Now.AddMonths(-1), Type = RecordType.Prescription, Title = "Ibuprofen", Description = "400mg twice a day" },
                new MedicalRecord { Id = 2, PatientId = 1, DoctorId = 101, DateCreated = DateTime.Now.AddMonths(-2), Type = RecordType.LabResult, Title = "Blood Count", Description = "Hemoglobin Normal" }
            );
        }
    }
}