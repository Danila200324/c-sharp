using Microsoft.AspNetCore.Mvc;
using Zadanie7.DAL;
using Zadanie7.DTO;
using Zadanie7.Services.IService;

namespace Zadanie7.Services

{
    [ApiController]
    [Route("api/trips")]
    public class TripController : ControllerBase
    {
        private ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public ActionResult<List<TripDto>> Read_all_Trips()
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, new JsonResult(
                    _tripService.GetTripsAsync().Result));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new JsonResult(
                    new { message = e.Message, Date = DateTime.Now }));
            }
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<ActionResult<int>> AddCustomer(int idTrip, ClientDtoCreate clientDtoCreate )
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, new JsonResult(
                    await _tripService.AddClientToTrip(idTrip, clientDtoCreate)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new JsonResult(
                    new { message = e.Message, Date = DateTime.Now }));
            }
        }
    }
}