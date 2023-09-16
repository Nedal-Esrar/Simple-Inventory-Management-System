using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SIMS;
using SIMS.Dtos;
using SIMS.Inventories;
using SIMS.Models;
using SIMS.Repositories;

var serviceProvider = new ServiceCollection()
  .AddScoped<IProductRepository, InMemoryProductRepository>()
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