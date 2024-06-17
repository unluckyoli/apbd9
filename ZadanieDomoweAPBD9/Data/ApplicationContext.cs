using Microsoft.EntityFrameworkCore;
using ZadanieDomoweAPBD9.Models;

namespace ZadanieDomoweAPBD9.Data;

public class ApplicationContext : DbContext
{
    
    protected ApplicationContext()
    {
    }
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "Cristiano", LastName = "Ronaldo", Email = "cris.ronal@gmail.com" },
            new Doctor { IdDoctor = 2, FirstName = "Leo", LastName = "Messi", Email = "leo.messi@gmail.com" }
        );
        
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { IdMedicament = 1, Name = "Ibuprom", Description = "zly", Type = "Type1" },
            new Medicament { IdMedicament = 2, Name = "Witamina C", Description = "super", Type = "Type2" }
        );

        modelBuilder.Entity<Patient>().HasData(
            new Patient { IdPatient = 1, FirstName = "Thiago", LastName = "Messi", Birthdate = new DateTime(1985, 4, 12) },
            new Patient { IdPatient = 2, FirstName = "Ciro", LastName = "Messi", Birthdate = new DateTime(1980, 3, 10) }
        );

        modelBuilder.Entity<Prescription>().HasData(
            new Prescription { IdPrescription = 1, Date = DateTime.Now, DueDate = DateTime.Now.AddDays(30), IdPatient = 1, IdDoctor = 1 },
            new Prescription { IdPrescription = 2, Date = DateTime.Now, DueDate = DateTime.Now.AddDays(30), IdPatient = 2, IdDoctor = 2 }
        );

        modelBuilder.Entity<PrescriptionMedicament>().HasData(
            new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 50, Details = "Raz dziennie" },
            new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 2, Dose = 30, Details = "graj w pilke" }
        );
        
    }
}

