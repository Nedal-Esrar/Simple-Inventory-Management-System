namespace SIMS.Dtos;

public class ProductDto
{
  public int Id { get; }
  
  public string Name { get; set; }
  
  public decimal Price { get; set; }
  
  public int Quantity { get; set; }
  
  public override string ToString() =>
    $"{Name}; Price: {Price:C}; Quantity: {Quantity}";
}