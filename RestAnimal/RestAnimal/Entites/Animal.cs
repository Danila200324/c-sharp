using System.ComponentModel.DataAnnotations;

namespace RestAnimal.Entites;

public class Animal
{
    public int IdAnimal { get; set; }
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [StringLength(200)] 
    public string Description { get; set; }
    [Required]
    [StringLength(200)]
    public string Category { get; set; }
    [Required]
    [StringLength(200)]
    public string Area { get; set; }

    public Animal(int idAnimal, string name, string description, string category, string area)
    {
        Area = area;
        IdAnimal = idAnimal;
        Name = name;
        Description = description;
        Category = category;
    }
}