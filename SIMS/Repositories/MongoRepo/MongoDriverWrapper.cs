using System.Linq.Expressions;
using MongoDB.Driver;
using SIMS.Models;

namespace SIMS.Repositories.MongoRepo;

public class MongoDriverWrapper : IMongoDriverWrapper
{
  public async Task<IEnumerable<Product>> Find(IMongoCollection<Product> collection, Expression<Func<Product, bool>> filter)
  {
    var products = await collection.FindAsync(filter);
    
    return await products.ToListAsync();
  }

  public async Task Add(IMongoCollection<Product> collection, Product product)
  {
    await collection.InsertOneAsync(product);
  }

  public async Task Update(IMongoCollection<Product> collection, Expression<Func<Product, bool>> filter, UpdateDefinition<Product> updateDefinition)
  {
    await collection.UpdateOneAsync(filter, updateDefinition);
  }

  public async Task Delete(IMongoCollection<Product> collection, Expression<Func<Product, bool>> filter)
  {
    await collection.DeleteOneAsync(filter);
  }
}