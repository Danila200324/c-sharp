using System.ComponentModel.DataAnnotations;

namespace Zadanie8.Dto;

public class LogginDtoRequest
{

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

}