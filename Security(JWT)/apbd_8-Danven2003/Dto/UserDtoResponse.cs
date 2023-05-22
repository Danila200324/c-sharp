using System.ComponentModel.DataAnnotations;

namespace Zadanie8.Dto;

public class UserDtoResponse
{
    public int IdUser { get; set; }
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public byte[] Password { get; set; }

}