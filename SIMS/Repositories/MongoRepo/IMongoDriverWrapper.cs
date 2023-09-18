using System.Linq.Expressions;
using MongoDB.Driver;
using SIMS.Models;

namespace SIMS.Repositories.MongoRepo;

public interface IMongoDriverWrapper
{
  Task<IEnumerable<Product>> Find(IMongoCollection<Product> collection, Expression<Func<Product, bool>> filter);

  Task Add(IMongoCollection<Product> collection, Product product);

  Task Update(IMongoCollection<Product> collection, Expression<Func<Product, bool>> filter, UpdateDefinition<Product> updateDefinition);

  Task Delete(IMongoCollection<Product> collection, Expression<Func<Product, bool>> filter);
}