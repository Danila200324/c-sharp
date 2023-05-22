namespace RestAnimal.Dto;

public class AnimalDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Area { get; set; }

    public AnimalDto(string name, string description, string category, string area)
    {
        Area = area;
        Name = name;
        Description = description;
        Category = category;
    }

    public AnimalDto()
    {
    }
}