
using MySqlConnector;
using RestAnimal.Dto;
using RestAnimal.Entites;
using RestAnimal.Services.IServices;

namespace RestAnimal.Services.IServiceAnimalImpl;

public class AnimalService : IAnimalService
{
    private const string ConnectionString = "server=localhost;uid=bestuser;password=bestuser;database=my_db";
    public List<Animal> GetAllAnimals(string orderBy)
    {
        var animals = new List<Animal>();
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = "SELECT * FROM Animal ORDER BY @OrderBy ";
            mySqlCommand.Parameters.AddWithValue("@OrderBy", orderBy);
            connection.Open();
            var reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                    animals.Add(new Animal((int)reader["IdAnimal"], reader["Name"].ToString() ?? string.Empty, 
                        reader["Description"].ToString() ?? string.Empty, reader["Category"].ToString() ?? string.Empty,
                        reader["Area"].ToString() ?? string.Empty));
            }
            connection.Close();
        }
        return animals;
    }

    public Animal GetAnimalById(int id)
    {
            Animal animal = null;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var mySqlCommand = new MySqlCommand();
                mySqlCommand.Connection = connection;
                mySqlCommand.CommandText = "SELECT * FROM Animal WHERE IdAnimal=@idAnimal";
                mySqlCommand.Parameters.AddWithValue("@idAnimal", id);
                connection.Open();
                var reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    animal = new Animal((int)reader["IdAnimal"], reader["Name"].ToString() ?? string.Empty,
                        reader["Description"].ToString() ?? string.Empty, reader["Category"].ToString() ?? string.Empty,
                        reader["Area"].ToString() ?? string.Empty);
                }

                connection.Close();
            }

            return animal ?? throw new InvalidOperationException("The animal with id " + id + " does not exist");
        }
    
    public string AddAnimal(AnimalDto animalDto)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = "INSERT INTO ANIMAL(Name, Description, Category, Area) values (@Name, @Description, @Category, @Area)";
            mySqlCommand.Parameters.AddWithValue("@Name", animalDto.Name);
            mySqlCommand.Parameters.AddWithValue("@Description", animalDto.Description);
            mySqlCommand.Parameters.AddWithValue("@Category", animalDto.Category);
            mySqlCommand.Parameters.AddWithValue("@Area", animalDto.Area);
            connection.Open();
            mySqlCommand.ExecuteScalar();
            connection.Close();
        }
        return "The animal has been created successfully";
    }
    public Animal UpdateAnimal(int id, AnimalDto animalDto)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = "UPDATE Animal SET name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @Id";
            mySqlCommand.Parameters.AddWithValue("@Name", animalDto.Name);
            mySqlCommand.Parameters.AddWithValue("@Description", animalDto.Description);
            mySqlCommand.Parameters.AddWithValue("@Category", animalDto.Category);
            mySqlCommand.Parameters.AddWithValue("@Area", animalDto.Area);
            mySqlCommand.Parameters.AddWithValue("@Id", id);
            connection.Open();
            mySqlCommand.ExecuteReader();
            connection.Close();
        }
        return GetAnimalById(id);
    }

    public string DeleteAnimal(int id)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = connection;
            mySqlCommand.CommandText = "DELETE FROM Animal WHERE IdAnimal = @Id";
            mySqlCommand.Parameters.AddWithValue("@Id", id);
            connection.Open();
            int rows = mySqlCommand.ExecuteNonQuery();
            connection.Close();
            if (rows == 0)
            {
                throw new Exception($"There is no animal with Id = {id}");
            }
        }
        return "The animal with id " + id + " has been deleted";
    }
}