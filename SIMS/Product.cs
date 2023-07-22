namespace SIMS;

public class Product : IReadOnlyProduct
{
  public Product(string name, decimal price, int quantity)
  {
    Name = name;

    Price = price;

    Quantity = quantity;
  }

  private string _name;

  public string Name
  {
    get => _name;

    set
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        throw new InvalidDataException("Empty String");
      }
      
      _name = value.Capitalize();
    }
  }

  private decimal _price;

  public decimal Price
  {
    get => _price;
    
    set
    {
      if (value <= 0)
      {
        throw new InvalidDataException("price <= 0");
      }

      _price = value;
    }
  }

  private int _quantity;

  public int Quantity
  {
    get => _quantity;
    
    set
    {
      if (value <= 0)
      {
        throw new InvalidDataException("quantity <= 0");
      }

      _quantity = value;
    }
  }

  public override string ToString() =>
    $"{Name}; Price: {Price:C}; Quantity: {Quantity}";
}