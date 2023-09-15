using SIMS.Dtos;

namespace SIMS.Inventories;

public interface IInventory
{
  Task AddProduct(ProductDto product);

  Task<IEnumerable<ProductDto>> GetAllProducts();

  Task UpdateProduct(ProductDto product);

  Task<ProductDto?> GetProductByName(string name);

  Task DeleteProduct(ProductDto product);
}