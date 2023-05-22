using System.ComponentModel.DataAnnotations;

namespace Zadanie8.Dto;

public class UserDtoRequest
{

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

}