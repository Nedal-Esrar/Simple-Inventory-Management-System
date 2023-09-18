using MongoDB.Driver;

namespace SIMS.Repositories.MongoRepo;

public interface IMongoCollectionProvider
{
  IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName);
}