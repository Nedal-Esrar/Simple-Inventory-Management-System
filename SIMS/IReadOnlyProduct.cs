namespace SIMS;

public interface IReadOnlyProduct
{
  string Name { get; }
  
  decimal Price { get; }
  
  int Quantity { get; }
}