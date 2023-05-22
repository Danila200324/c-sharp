using Microsoft.EntityFrameworkCore;
using Zadanie8.Models;

namespace Zadanie8.Services.ServicesIml;

public class DoctorServiceImpl : IDoctorService
{

    private readonly MainDbContext _mainDbContext;

    public DoctorServiceImpl(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }

    public async Task<DoctorDtoResponse> CreateDoctor(DoctorDtoRequest doctorDtoRequest)
    {
        var doctor = MapToDoctorFromRequest(doctorDtoRequest);
        _mainDbContext.Doctors.Add(doctor);
        await _mainDbContext.SaveChangesAsync();
        return MapToDoctorDtoResponse(doctor);
    }

    public async Task<string> DeleteDoctor(int idDoctor)
    {
        var doctor = await _mainDbContext.Doctors.Include(d => d.Prescriptions)
            .SingleOrDefaultAsync(d => d.IdDoctor == idDoctor);
        if (doctor == null) throw new Exception("No such doctor");
        
        _mainDbContext.Doctors.Remove(doctor);
        await _mainDbContext.SaveChangesAsync();

        return "Doctor has been deleted successfully";
    }
    

    public async Task<List<DoctorDtoResponse>> GetAllDoctors()
    {
        var doctors = await _mainDbContext.Doctors.ToListAsync();

        return doctors.Select(MapToDoctorDtoResponse).ToList();
    }

    public async Task<DoctorDtoResponse> GetDoctorById(int idDoctor)
    {
        var doctor = await _mainDbContext.Doctors.Where(doc => doc.IdDoctor == idDoctor).FirstAsync();
        if (doctor == null) throw new Exception($"No doctor with id: {idDoctor}");
        return MapToDoctorDtoResponse(doctor);
    }

    public async Task<DoctorDtoResponse> UpdateDoctor(int idDoctor, DoctorDtoRequest doctorDtoRequest)
    {
        var doctor = await _mainDbContext.Doctors.FindAsync(idDoctor);
        
        if (doctor == null)
        {
            throw new Exception($"No doctor with id: {idDoctor}");
        }
        
        doctor.FirstName = doctorDtoRequest.FirstName;
        doctor.LastName = doctorDtoRequest.LastName;
        doctor.Email = doctorDtoRequest.Email;
        await _mainDbContext.SaveChangesAsync();

        return MapToDoctorDtoResponse(doctor);
    }

    public DoctorDtoResponse MapToDoctorDtoResponse(Doctor doctor)
    {
        return new DoctorDtoResponse
        {
            IdDoctor = doctor.IdDoctor,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Email = doctor.Email
        };
    }
    
    public Doctor MapToDoctorFromRequest(DoctorDtoRequest doctorDtoRequest)
    {
        return new Doctor
        {
            FirstName = doctorDtoRequest.FirstName,
            LastName = doctorDtoRequest.LastName,
            Email = doctorDtoRequest.Email
        };
    }
    
    
    

}