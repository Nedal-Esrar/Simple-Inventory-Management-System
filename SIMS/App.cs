using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIMS;
using SIMS.Dtos;
using SIMS.Inventories;
using SIMS.Models;
using SIMS.Repositories;
using SIMS.Repositories.SqlRepo;

var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .Build();

var serviceProvider = new ServiceCollection()
  .AddScoped<ISqlConnectionFactory>(_ => new SqlConnectionFactory(configuration.GetConnectionString("SqlConnection")))
  .AddScoped<IDapperWrapper, DapperWrapper>()
  .AddScoped<IProductRepository, SqlProductRepository>()
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