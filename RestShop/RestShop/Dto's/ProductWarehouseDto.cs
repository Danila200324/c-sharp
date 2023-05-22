namespace RestShop.Dto_s;

public class ProductWarehouseDto
{
    public int IdWarehouse { get; set; }
    
    public int IdProduct { get; set; }
    
    public int IdOrder { get; set; }
    
    public int Amount { get; set; }

    public DateTime CreatedAt { get; set; }
}