using RestAnimal.Dto;
using RestAnimal.Entites;

namespace RestAnimal.Services.IServices;

public interface IAnimalService
{
    public List<Animal> GetAllAnimals(string orderBy);

    public Animal GetAnimalById(int id);

    public string AddAnimal(AnimalDto animalDto);

    public Animal UpdateAnimal(int id, AnimalDto animalDto);

    public string DeleteAnimal(int id);
}