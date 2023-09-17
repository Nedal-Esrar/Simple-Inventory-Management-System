using MongoDB.Driver;

namespace SIMS.Repositories.MongoRepo;

public interface IMongoCollectionFactory
{
  IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName);
}