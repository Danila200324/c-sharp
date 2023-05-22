using System.Data;
using MySqlConnector;
using RestShop.Dto_s;
using RestShop.Entities;
using RestShop.Services.IServices;

namespace RestShop.Services.IServicesIml;

public class ProcedureProductWarehouseServiceImpl : IProductWarehouseService
{
    private const string ConnectionString = "server=localhost;uid=bestuser;password=bestuser;database=my_db";

    public async Task<ProductWarehouse> AddProductWarehouse(ProductWarehouseDto productWarehouseDto)
    {
        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            using (MySqlCommand cmd = new MySqlCommand("AddProductToWarehouse", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add("@IdProduct", MySqlDbType.Int32).Value = productWarehouseDto.IdProduct;
                cmd.Parameters.Add("@IdWarehouse", MySqlDbType.Int32).Value = productWarehouseDto.IdWarehouse;
                cmd.Parameters.Add("@Amount", MySqlDbType.Int32).Value = productWarehouseDto.Amount;
                cmd.Parameters.Add("@CreatedAt", MySqlDbType.DateTime).Value = DateTime.Now;
                
                cmd.Parameters.Add(new MySqlParameter("@LastInsertedId", MySqlDbType.Int32));
                cmd.Parameters["@LastInsertedId"].Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteNonQuery();
                return GetProductWarehouseById();
            }
        }
    }

    private ProductWarehouse GetProductWarehouseById()
    {
        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            using (MySqlCommand cmd =
                   new MySqlCommand("SELECT * FROM product_warehouse WHERE IdProductWarehouse = (SELECT MAX(IdProductWarehouse) FROM product_warehouse)", conn))
            {

                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ProductWarehouse productWarehouse = new ProductWarehouse();

                        productWarehouse.IdProductWarehouse = Convert.ToInt32(reader["IdProductWarehouse"]);
                        productWarehouse.IdProduct = Convert.ToInt32(reader["IdProduct"]);
                        productWarehouse.Amount = Convert.ToInt32(reader["Amount"]);
                        productWarehouse.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                        productWarehouse.Price = Convert.ToDouble(reader["Price"]);
                        productWarehouse.IdOrder = Convert.ToInt32(reader["IdOrder"]);
                        productWarehouse.IdWarehouse = Convert.ToInt32(reader["IdWarehouse"]);

                        return productWarehouse;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}