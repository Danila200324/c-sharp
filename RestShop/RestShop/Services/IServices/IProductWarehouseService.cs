using RestShop.Dto_s;
using RestShop.Entities;

namespace RestShop.Services.IServices;

public interface IProductWarehouseService
{
    public Task<ProductWarehouse> AddProductWarehouse(ProductWarehouseDto productWarehouseDto);
}