namespace Zadanie8.Services;

public interface IDoctorService
{
    Task<DoctorDtoResponse> GetDoctorById(int idDoctor);

    Task<List<DoctorDtoResponse>> GetAllDoctors();

    Task<DoctorDtoResponse> UpdateDoctor(int idDoctor, DoctorDtoRequest doctorDtoRequest);

    Task<DoctorDtoResponse> CreateDoctor(DoctorDtoRequest doctorDtoRequest);

    Task<string> DeleteDoctor(int idDoctor);
}