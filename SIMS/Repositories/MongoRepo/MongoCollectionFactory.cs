using MongoDB.Driver;

namespace SIMS.Repositories.MongoRepo;

public class MongoCollectionFactory : IMongoCollectionFactory
{
  private readonly IMongoDatabase _mongoDatabase;

  public MongoCollectionFactory(IMongoDatabase mongoDatabase)
  {
    _mongoDatabase = mongoDatabase;
  }
  
  public IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName)
  {
    return _mongoDatabase.GetCollection<TDocument>(collectionName);
  }
}