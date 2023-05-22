
namespace Zadanie8.Models;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int IdUser { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public byte[] Salt { get; set; }

    [Required]
    public byte[] PasswordHash { get; set; }
}

