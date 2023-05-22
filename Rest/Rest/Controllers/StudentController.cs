

using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entity;
using WebApplication2.Services.IService;
using WebApplication2.Services.ServiceImpl;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService = new StudentServiceImpl();

    [HttpGet]
    public ActionResult<List<Student>> GetAllStudents()
    {
        try
        {
            return Ok(new JsonResult(_studentService.GetAll()));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new JsonResult(new { message = e.Message, Date = DateTime.Now}));
        }
    }
    
    [HttpGet("{id}")]
    public ActionResult<Student> GetStudentById(string id)
    {
        try
        {
            return Ok(new JsonResult(_studentService.GetStudentById(id)));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status302Found, new JsonResult(new { message = e.Message, Date = DateTime.Now}));
        }
    }
    
    [HttpPost]
    public ActionResult<Student> AddStudent(Student student)
    {
        try
        {
            return StatusCode(StatusCodes.Status201Created,new JsonResult(_studentService.AddStudent(student)));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new JsonResult(new
            { message = e.Message, Date = DateTime.Now }));
        }
    }

    [HttpPut]
    public ActionResult<Student> UpdateStudent(string id, Student student)
    {
        try
        {
            return Ok(new JsonResult(_studentService.UpdateStudent(id, student)));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new JsonResult(new { message = e.Message,
                Date = DateTime.Now}));
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteStudent(string id)
    {
        try
        {
            _studentService.DeleteStudent(id);
            return Ok(new JsonResult(new {message = "The student with id " + id + " was deleted"}));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new JsonResult(new { message = e.Message, Date = DateTime.Now}));
        }
    }
}