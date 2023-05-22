using Microsoft.EntityFrameworkCore;

namespace Zadanie8.Models;

public class MainDbContext : DbContext
{

    public MainDbContext(DbContextOptions<MainDbContext> options) : base()
    { }

    protected MainDbContext()
    {}
    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Prescription> Prescriptions { get; set; }

    public DbSet<Medicament> Medications { get; set; }

    public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=HOSPITAL;User Id=sa;Password=danven2003;TrustServerCertificate=true;");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription_Medicament>()
            .HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });

        modelBuilder.Entity<Doctor>().HasData(
        new Doctor { IdDoctor = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" },
        new Doctor { IdDoctor = 2, FirstName = "Jane", LastName = "Smith", Email = "janesmith@example.com" }
        );

        modelBuilder.Entity<Patient>().HasData(
        new Patient { IdPatient = 1, FirstName = "Alice", LastName = "Anderson", Birthdate = new DateTime(1990, 1, 1) },
        new Patient { IdPatient = 2, FirstName = "Bob", LastName = "Brown", Birthdate = new DateTime(1995, 2, 2) }
        );

        modelBuilder.Entity<Medicament>().HasData(
        new Medicament { IdMedicament = 1, Name = "Medicament 1", Description = "Description for Medicament 1", Type = "Type 1" },
        new Medicament { IdMedicament = 2, Name = "Medicament 2", Description = "Description for Medicament 2", Type = "Type 2" }
        );

        modelBuilder.Entity<Prescription>().HasData(
        new Prescription { IdPrescription = 1, Date = new DateTime(2022, 1, 1), DueDate = new DateTime(2022, 2, 1), IdDoctor = 1, IdPatient = 1 },
        new Prescription { IdPrescription = 2, Date = new DateTime(2022, 2, 1), DueDate = new DateTime(2022, 3, 1), IdDoctor = 2, IdPatient = 2 }
        );

        modelBuilder.Entity<Prescription_Medicament>().HasData(
        new Prescription_Medicament { IdPrescription = 1, IdMedicament = 1, Dose = 1, Details = "Details for Medicament 1" },
        new Prescription_Medicament { IdPrescription = 2, IdMedicament = 2, Dose = 2, Details = "Details for Medicament 2" }
        );
    }
}