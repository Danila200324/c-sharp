using MySqlConnector;
using RestShop.Dto_s;
using RestShop.Entities;
using RestShop.Services.IServices;

namespace RestShop.Services.IServicesIml;

public class ProductWarehouseServiceImpl : IProductWarehouseService
{

    private const string ConnectionString = "server=localhost;uid=bestuser;password=bestuser;database=my_db";

    public async Task<ProductWarehouse> AddProductWarehouse(ProductWarehouseDto productWarehouseDto)
    {
        int i;
        ProductWarehouse productWarehouse = null;

        if (productWarehouseDto.Amount <= 0)
        {
            throw new Exception("The amount can't be below zero");
        }

        using (var connection = new MySqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            using (var transaction = await connection.BeginTransactionAsync())
            {
                try
                {
                    await CheckProductById(productWarehouseDto, connection, transaction);
                    await CheckWarehouseById(productWarehouseDto, connection, transaction);
                    await CheckOrderByProduct(productWarehouseDto, connection, transaction);
                    await CheckProductWarehouseById(productWarehouseDto, connection, transaction);
                    await UpdateFulfilledInOrder(productWarehouseDto, connection, transaction);

                    i = await InsertProductWarehouseAsync(productWarehouseDto, connection, transaction);
                    await transaction.CommitAsync();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    Console.WriteLine(exception.Message);
                    throw new Exception(exception.Message);
                }
            }
        }

        return await GetProductWarehouseById(i);
        ;
    }

    private async Task<ProductWarehouse> GetProductWarehouseById(int id)
    {
        ProductWarehouse productWarehouse = null;
        await using (var connection = new MySqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Product_Warehouse where IdProductWarehouse = @id";
            command.Parameters.AddWithValue("@id", id);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                productWarehouse = new ProductWarehouse(
                    Convert.ToInt32(reader["IdProductWarehouse"].ToString()),
                    Convert.ToInt32(reader["IdWarehouse"].ToString()),
                    Convert.ToInt32(reader["IdProduct"].ToString()),
                    Convert.ToInt32(reader["IdOrder"].ToString()),
                    Convert.ToInt32(reader["Amount"].ToString()),
                    Convert.ToDouble(reader["Price"].ToString()),
                    Convert.ToDateTime(reader["CreatedAt"].ToString())
                );
            }
        }

        return productWarehouse ?? throw new InvalidOperationException();
    }

    private async Task UpdateFulfilledInOrder(ProductWarehouseDto productWarehouseDto,
        MySqlConnection connection, MySqlTransaction transaction)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = "UPDATE `Order` SET FulfilledAt = @fulfilledAt WHERE IdOrder = @idOrder";
        command.Parameters.AddWithValue("@fulfilledAt", DateTime.Now);
        command.Parameters.AddWithValue("@idOrder", productWarehouseDto.IdOrder);
        await command.ExecuteNonQueryAsync();
    }

    private async Task CheckProductWarehouseById(ProductWarehouseDto productWarehouseDto,
        MySqlConnection connection, MySqlTransaction transaction)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = "SELECT * FROM Product_Warehouse WHERE IdOrder = @idOrder";
        command.Parameters.AddWithValue("@idOrder", productWarehouseDto.IdOrder);
        var exist = (int)(await command.ExecuteScalarAsync() ?? 0);
        if (exist != 0)
        {
            throw new Exception($"The Product_Warehouse with order id {productWarehouseDto.IdProduct} already exist");
        }
    }

    private async Task CheckOrderByProduct(ProductWarehouseDto productWarehouseDto,
        MySqlConnection connection, MySqlTransaction transaction)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            "SELECT * FROM `Order` WHERE IdOrder = @idOrder AND IdProduct = @idProduct AND Amount = @amount AND CreatedAt < @createdAt";
        command.Parameters.AddWithValue("@idOrder", productWarehouseDto.IdOrder);
        command.Parameters.AddWithValue("@idProduct", productWarehouseDto.IdProduct);
        command.Parameters.AddWithValue("@amount", productWarehouseDto.Amount);
        command.Parameters.AddWithValue("@createdAt", DateTime.Now);
        var exist = (int)(await command.ExecuteScalarAsync() ?? 0);
        if (exist == 0)
        {
            throw new Exception($"The Product_warehouse doesn't belong to any order");
        }
    }


    private async Task CheckProductById(ProductWarehouseDto productWarehouseDto,
        MySqlConnection connection, MySqlTransaction transaction)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = "SELECT * FROM Product WHERE IdProduct = @idProduct";
        command.Parameters.AddWithValue("@idProduct", productWarehouseDto.IdProduct);
        var exist = (int)(await command.ExecuteScalarAsync() ?? 0);
        if (exist == 0)
        {
            throw new Exception($"The product wth id {productWarehouseDto.IdProduct} doesn't exist");
        }
    }

    private async Task CheckWarehouseById(ProductWarehouseDto productWarehouseDto,
        MySqlConnection connection, MySqlTransaction transaction)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = "SELECT * FROM Warehouse WHERE IdWarehouse = @idWarehouse";
        command.Parameters.AddWithValue("@idWarehouse", productWarehouseDto.IdWarehouse);
        var exist = (int)(await command.ExecuteScalarAsync() ?? 0);
        if (exist == 0)
        {
            throw new Exception($"The warehouse wth id {productWarehouseDto.IdWarehouse} doesn't exist");
        }
    }

    private async Task<int> InsertProductWarehouseAsync(ProductWarehouseDto productWarehouseDto,
        MySqlConnection connection, MySqlTransaction transaction)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            "INSERT INTO Product_Warehouse (idProduct, Amount, Price, idWarehouse, idOrder, CreatedAt)" +
            "VALUES (@productId, @amount, @price, @warehouseId, @orderId, @createdAt)";
        command.Parameters.AddWithValue("@productId", productWarehouseDto.IdProduct);
        command.Parameters.AddWithValue("@amount", productWarehouseDto.Amount);
        command.Parameters.AddWithValue("@price",
            productWarehouseDto.Amount * GetProductPrice(productWarehouseDto).Result);
        command.Parameters.AddWithValue("@warehouseId", productWarehouseDto.IdWarehouse);
        command.Parameters.AddWithValue("@orderId", productWarehouseDto.IdOrder);
        command.Parameters.AddWithValue("@createdAt", DateTime.Now);
        int rowsAffected = await command.ExecuteNonQueryAsync();

        if (rowsAffected <= 0)
        {
            throw new Exception("The problems with inserting");
        }

        return (int)command.LastInsertedId;
    }

    private async Task<double> GetProductPrice(ProductWarehouseDto productWarehouseDto)
    {
        double price = 0;
        await using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT Price FROM Product WHERE IdProduct = @idProduct";
        command.Parameters.AddWithValue("@idProduct", productWarehouseDto.IdProduct);
        var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            price = Convert.ToDouble(reader["Price"]);
            Console.WriteLine(price);
        }

        return price;
    }
}