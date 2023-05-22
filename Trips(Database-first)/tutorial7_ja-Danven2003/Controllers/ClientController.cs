using Microsoft.AspNetCore.Mvc;
using Zadanie7.DAL;
using Zadanie7.DTO;
using Zadanie7.Services.IService;

namespace Zadanie7.Services

{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteClient(int id)
        {
            try
            {
                await _clientService.DeleteClient(id);
                return StatusCode(StatusCodes.Status201Created, new JsonResult(
                    new { message = $"The client with id {id} has been deleted" }));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new JsonResult(
                    new { message = e.Message, Date = DateTime.Now }));
            }
        }

    }
}