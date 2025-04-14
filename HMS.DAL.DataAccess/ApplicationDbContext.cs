using HMS.DAL.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace HMS.DAL.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        #region Constractor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #endregion Constractor

        #region tables

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Billing> Billings { get; set; }

        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<BillingStatus> BillingStatuses { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Specialty> Specialty { get; set; }

        #endregion tables

        #region FluentAPI

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.AppointmentStatus)
                .WithMany()
                .HasForeignKey(a => a.AppointmentStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MedicalRecord>()
            .HasOne(mr => mr.Patient)
            .WithMany()
            .HasForeignKey(mr => mr.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MedicalRecord>()
            .HasOne(mr => mr.Doctor)
            .WithMany()
            .HasForeignKey(mr => mr.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Patient>()
                .HasOne(mr => mr.User)
                .WithMany()
                .HasForeignKey(mr => mr.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Patient>()
                .HasOne(mr => mr.Gender)
                .WithMany()
                .HasForeignKey(mr => mr.GenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Doctor>()
                .HasOne(mr => mr.User)
                .WithMany()
                .HasForeignKey(mr => mr.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Doctor>()
                .HasOne(mr => mr.Specialty)
                .WithMany()
                .HasForeignKey(mr => mr.SpecialtyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Billing>()
                .HasOne(mr => mr.Patient)
                .WithMany()
                .HasForeignKey(mr => mr.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Billing>()
                .HasOne(mr => mr.Status)
                .WithMany()
                .HasForeignKey(mr => mr.BillingStatusId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        #endregion FluentAPI
    }
}