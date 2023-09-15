using SIMS.Models;

namespace SIMS.Repositories;

public class InMemoryProductRepository : IProductRepository
{
  public Task<IEnumerable<Product>> GetAll()
  {
    throw new NotImplementedException();
  }

  public Task<Product?> GetByName(string name)
  {
    throw new NotImplementedException();
  }

  public Task Add(Product product)
  {
    throw new NotImplementedException();
  }

  public Task Update(Product product)
  {
    throw new NotImplementedException();
  }

  public Task Delete(Product product)
  {
    throw new NotImplementedException();
  }
}