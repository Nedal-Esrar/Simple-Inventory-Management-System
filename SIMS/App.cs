using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SIMS;
using SIMS.Inventories;
using SIMS.Repositories;

var serviceProvider = new ServiceCollection()
  .AddScoped<IProductRepository, InMemoryProductRepository>()
  .AddScoped<IMapper>(_ => new MapperConfiguration(_ => { }).CreateMapper())
  .AddScoped<IInventory, Inventory>()
  .AddScoped<InventoryManagementSystem>();

var app = serviceProvider
  .BuildServiceProvider()
  .GetRequiredService<InventoryManagementSystem>();

await app.Run();