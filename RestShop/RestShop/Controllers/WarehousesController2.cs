
using Microsoft.AspNetCore.Mvc;
using RestShop.Dto_s;
using RestShop.Entities;
using RestShop.Services.IServices;

namespace RestShop.Controllers;

[ApiController]
[Route("api/warehouses2")]
public class WarehousesController2 : ControllerBase
{

    private IProductWarehouseService _productWarehouseService;

    public WarehousesController2(IProductWarehouseService productWarehouseService)
    {
        _productWarehouseService = productWarehouseService;
    }
    
    [HttpPost]
    public ActionResult<ProductWarehouse> AddStudent(ProductWarehouseDto productWarehouseDto)
    {
        try
        {
            return StatusCode(StatusCodes.Status201Created,new JsonResult(_productWarehouseService.AddProductWarehouse(productWarehouseDto).Result));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new JsonResult(new
                { message = e.Message, Date = DateTime.Now }));
        }
    }
}