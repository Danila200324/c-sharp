using Microsoft.AspNetCore.Mvc;
using Zadanie8.Services;

namespace Zadanie8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDtoResponse>> GetDoctorById(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorById(id);
                return Ok(doctor);
            } 
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new JsonResult(
                    new { message = e.Message, Date = DateTime.Now }));
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<DoctorDtoResponse>> GetAllDoctors()
        {
            try
            {
                var doctors = await _doctorService.GetAllDoctors();
                return Ok(doctors);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResult(
                    new { message = e.Message, Date = DateTime.Now }));
            }
        }
        
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteDoctorById(int id)
        {
            try
            {
                var responseMessage = await _doctorService.DeleteDoctor(id);
                return Ok(responseMessage);
            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new JsonResult(
                    new { message = e.Message, Date = DateTime.Now }));
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> CreateDoctor(DoctorDtoRequest doctorDtoRequest)
        {
            try
            {
                var newDoctor = await _doctorService.CreateDoctor(doctorDtoRequest);
                return Ok(newDoctor);
            }catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResult(
                    new { message = e.Message, Date = DateTime.Now }));
            }
        }
        
        [HttpPut("{idDoctor}")]
        public async Task<ActionResult<DoctorDtoResponse>> UpdateDoctor(int idDoctor, DoctorDtoRequest doctorDtoRequest)
        {
            try
            {
                var responseMessage = await _doctorService.UpdateDoctor(idDoctor, doctorDtoRequest);
                return Ok(responseMessage);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new JsonResult(
                    new { message = e.Message, Date = DateTime.Now }));
            }
        }
    }
}