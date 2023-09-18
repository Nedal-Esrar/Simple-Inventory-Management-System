using MongoDB.Driver;
using SIMS.Models;

namespace SIMS.Repositories.MongoRepo;

public class MongoDbProductRepository : IProductRepository
{
  private readonly IMongoCollectionProvider _collectionProvider;

  private readonly IMongoDriverWrapper _mongoDriverWrapper;

  public MongoDbProductRepository(IMongoCollectionProvider collectionProvider, IMongoDriverWrapper mongoDriverWrapper)
  {
    _collectionProvider = collectionProvider;
    
    _mongoDriverWrapper = mongoDriverWrapper;
  }
  
  public async Task<IEnumerable<Product>> GetAll()
  {
    var collection = GetProductsCollection();
    
    var products = await _mongoDriverWrapper.Find(collection, _ => true);

    return products;
  }

  private IMongoCollection<Product> GetProductsCollection()
  {
    return _collectionProvider.GetCollection<Product>(MongoEntitiesNames.ProductsCollectionName);
  }

  public async Task<Product?> GetByName(string name)
  {
    var collection = GetProductsCollection();
        
    var product = await _mongoDriverWrapper.Find(collection, p => p.Name == name);
    
    return product.FirstOrDefault();
  }

  public async Task Add(Product product)
  {
    var collection = GetProductsCollection();

    await _mongoDriverWrapper.Add(collection, product);
  }

  public async Task Update(Product product)
  {
    var updateDefinition = Builders<Product>.Update
      .Set(p => p.Name, product.Name)
      .Set(p => p.Price, product.Price)
      .Set(p => p.Quantity, product.Quantity);
    
    var collection = GetProductsCollection();

    await _mongoDriverWrapper.Update(collection, p => p.Id == product.Id, updateDefinition);
  }

  public async Task Delete(Product product)
  {
    var collection = GetProductsCollection();

    await _mongoDriverWrapper.Delete(collection, p => p.Id == product.Id);
  }
}