namespace SIMS.Exceptions;

public class ProductAlreadyExistsException : Exception
{
  public ProductAlreadyExistsException() : base("Product Already Exists")
  {
  }
}