using MongoDB.Driver;

namespace SIMS.Repositories.MongoRepo;

public class MongoCollectionProvider : IMongoCollectionProvider
{
  private readonly IMongoDatabase _mongoDatabase;

  public MongoCollectionProvider(IMongoDatabase mongoDatabase)
  {
    _mongoDatabase = mongoDatabase;
  }
  
  public IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName)
  {
    return _mongoDatabase.GetCollection<TDocument>(collectionName);
  }
}