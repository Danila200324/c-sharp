using System.Net;
using Microsoft.AspNetCore.Mvc;
using RestAnimal.Dto;
using RestAnimal.Entites;
using RestAnimal.Services.IServices;

namespace RestAnimal.Controllers;

[ApiController]
[Route("api/animals")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }
    
    [HttpGet]
    public ActionResult<List<Animal>> GetAllStudents(string? orderBy)
    {
        try
        {
            return Ok(new JsonResult(_animalService.GetAllAnimals(orderBy ??= "Name")));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new JsonResult(HttpStatusCode.BadRequest, new { message = e.Message, Date = DateTime.Now }));
        }
    }
    
    [HttpGet("{id}")]
    public ActionResult<Animal> GetStudentById(int id)
    {
        try
        {
            return Ok(new JsonResult(_animalService.GetAnimalById(id)));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new JsonResult(new { message = e.Message, Date = DateTime.Now}));
        }
    }
    
    [HttpPost]
    public ActionResult<Animal> AddAnimal(AnimalDto animalDto)
    {
        try
        {
            return Ok(new JsonResult(_animalService.AddAnimal(animalDto)));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new JsonResult(new { message = e.Message, Date = DateTime.Now}));
        }
    }
    
    [HttpPut]
    public ActionResult<Animal> UpdateAnimal(int id, AnimalDto animalDto)
    {
        try
        {
            return Ok(new JsonResult(_animalService.UpdateAnimal(id, animalDto)));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new JsonResult(new { message = e.Message, Date = DateTime.Now}));
        }
    }
    
    [HttpDelete]
    public ActionResult<string> DeleteAnimal(int id)
    {
        try
        {
            return Ok(new JsonResult(_animalService.DeleteAnimal(id)));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new JsonResult(new { message = e.Message, Date = DateTime.Now}));
        }
    }
}