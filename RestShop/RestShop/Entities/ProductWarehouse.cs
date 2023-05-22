namespace RestShop.Entities;

public class ProductWarehouse
{
    public int IdProductWarehouse { get; set; }
    
    public int IdWarehouse { get; set; }
    
    public int IdProduct { get; set; }
    
    public int IdOrder { get; set; }
    
    public int Amount { get; set;}
    
    public double Price { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public ProductWarehouse(int idProductWarehouse, int idWarehouse, int idProduct, int idOrder, int amount, double price, DateTime createdAt)
    {
        IdProductWarehouse = idProductWarehouse;
        IdWarehouse = idWarehouse;
        IdProduct = idProduct;
        IdOrder = idOrder;
        Amount = amount;
        Price = price;
        CreatedAt = createdAt;
    }
    public ProductWarehouse(){}

    public override string ToString()
    {
        return base.ToString();
    }
}