using Microsoft.EntityFrameworkCore;
using Zadanie8.Dto;
using Zadanie8.Models;

namespace Zadanie8.Services.ServicesIml;

public class PrescriptionServiceImpl : IPrescriptionService
{
    private readonly MainDbContext _mainDbContext;

    public PrescriptionServiceImpl(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }
    
    public async Task<PrescriptionDtoResponse> GetPrescriptionById(int id)
    {
        var prescription = await _mainDbContext.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Include(p => p.Prescription_Medicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPrescription == id);

        if (prescription == null)
        {
            throw new Exception($"No prescription with id: {id}");
        }
        
        var prescriptionDtoResponse = new PrescriptionDtoResponse
        {
            IdPrescription = prescription.IdPrescription,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            Doctor = new DoctorDtoResponse
            {
                IdDoctor = prescription.Doctor.IdDoctor,
                FirstName = prescription.Doctor.FirstName,
                LastName = prescription.Doctor.LastName,
                Email = prescription.Doctor.Email
            },
            Patient = new PatientDtoResponse
            {
                IdPatient = prescription.Patient.IdPatient,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                Birthdate = prescription.Patient.Birthdate
            },
            Medicaments = prescription.Prescription_Medicaments.Select(pm => new MedicamentDtoResponse
            {
                IdMedicament = pm.Medicament.IdMedicament,
                Name = pm.Medicament.Name,
                Description = pm.Medicament.Description,
                Type = pm.Medicament.Type
            }).ToList()
        };

        return prescriptionDtoResponse;
    }
}