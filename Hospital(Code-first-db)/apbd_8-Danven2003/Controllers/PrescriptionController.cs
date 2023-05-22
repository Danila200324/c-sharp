using Microsoft.AspNetCore.Mvc;
using Zadanie8.Dto;
using Zadanie8.Services;

namespace Zadanie8.Controllers;


[ApiController]
[Route("[controller]")]
public class PrescriptionController : ControllerBase
{
    
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<PrescriptionDtoResponse>> GetPrescriptionById(int id)
    {
        try
        {
            var prescription = await _prescriptionService.GetPrescriptionById(id);
            return Ok(prescription);
        } catch (Exception e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new JsonResult(
                new { message = e.Message, Date = DateTime.Now }));
        }
    }
}