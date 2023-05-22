using Zadanie8.Dto;

namespace Zadanie8.Services;

public interface IPrescriptionService
{
    Task<PrescriptionDtoResponse> GetPrescriptionById(int id);
}