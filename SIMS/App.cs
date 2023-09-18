using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SIMS;
using SIMS.Dtos;
using SIMS.Inventories;
using SIMS.Models;
using SIMS.Repositories;
using SIMS.Repositories.MongoRepo;

var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .Build();

var serviceProvider = new ServiceCollection()
  .AddScoped<IMongoCollectionProvider>(_ =>
  {
    var client = new MongoClient(configuration.GetConnectionString("MongoConnection"));

    var database = client.GetDatabase(MongoEntitiesNames.DatabaseName);

    return new MongoCollectionProvider(database);
  })
  .AddScoped<IMongoDriverWrapper, MongoDriverWrapper>()
  .AddScoped<IProductRepository, MongoDbProductRepository>()
  .AddScoped<IMapper>(_ => new MapperConfiguration(cfg =>
  {
    cfg.CreateMap<Product, ProductDto>();
    cfg.CreateMap<ProductDto, Product>();
  }).CreateMapper())
  .AddScoped<IInventory, Inventory>()
  .AddScoped<InventoryManagementSystem>();

var app = serviceProvider
  .BuildServiceProvider()
  .GetRequiredService<InventoryManagementSystem>();

await app.Run();