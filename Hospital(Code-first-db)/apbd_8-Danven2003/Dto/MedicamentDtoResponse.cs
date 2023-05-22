using System.ComponentModel.DataAnnotations;

namespace Zadanie8.Dto;

public class MedicamentDtoResponse
{
    public int IdMedicament { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
    [Required]
    [MaxLength(100)]
    public string Type { get; set; }
}