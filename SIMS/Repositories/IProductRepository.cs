using SIMS.Models;

namespace SIMS.Repositories;

public interface IProductRepository
{
  Task<IEnumerable<Product>> GetAll();

  Task<Product?> GetByName(string name);

  Task Add(Product product);

  Task Update(Product product);

  Task Delete(Product product);
}