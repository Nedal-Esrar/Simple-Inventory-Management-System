using SIMS.Exceptions;
using SIMS.Inventories;
using Utils = SIMS.Utilities;

namespace SIMS;

public class InventoryManagementSystem
{
  private readonly IInventory _inventory;

  public InventoryManagementSystem(IInventory inventory)
  {
    _inventory = inventory;
  }
  
  public async Task Run()
  {
    Console.WriteLine("Welcome to Simple Inventory Management System!");
    
    Utils.DisplayStars();

    Console.WriteLine("Note: For any invalid input, you will be prompted again to enter a correct value.");

    bool exit = false;

    while (!exit)
    {
      Utils.DisplayStars();
      
      Console.WriteLine("Please pick a choice [1..6]");

      Utils.DisplayChoicesMenu();
      
      Utils.DisplayStars();

      string choice = Utils.GetStringInput("Choice");
      
      Utils.DisplayStars();

      exit = await HandleChoice(choice);
    }
  }

  /// <summary>
  /// Chooses the appropriate handler according to the choice.
  /// returns a boolean that indicates whether to terminate the application or not.
  /// </summary>
  /// <param name="choice"></param>
  /// <returns>
  /// True if the choice was exit choice (6);
  /// False otherwise.
  /// </returns>
  private async Task<bool> HandleChoice(string choice)
  {
    switch (choice)
    {
      case "1":
        await HandleAddChoice();
        
        break;
      case "2":
        await HandleViewAllChoice();
        
        break;
      case "3":
        await HandleEditChoice();
        
        break;
      case "4":
        await HandleDeleteChoice();
        
        break;
      case "5":
        await HandleSearchChoice();
        
        break;
      case "6":
        Console.WriteLine("Thank you for using Simple Inventory Management System ;)\nExiting now. Goodbye!");

        return true;
      default:
        Console.WriteLine("the choice is not in the range [1..6]");
        
        break;
    }

    return false;
  }

  private async Task HandleAddChoice()
  {
    Console.WriteLine("Please enter the information of the product you want to add.");

    string name = Utils.GetStringInput("Name").Capitalize();

    decimal price = Utils.GetPriceInput();

    int quantity = Utils.GetQuantityInput();
    
    Utils.DisplayStars();

    try
    {
      await _inventory.AddProduct(new()
      {
        Name = name,
        Price = price,
        Quantity = quantity
      });
      
      Console.WriteLine("The product has been added successfully.");
    }
    catch (ProductAlreadyExistsException)
    {
      Console.WriteLine("A product with the same name exists in the inventory; no changes were made.");
    }
  }

  private async Task HandleViewAllChoice()
  {
    var products = await _inventory.GetAllProducts();

    if (products.Any())
    {
      int order = 1;
      
      foreach (var product in products)
      {
        Console.WriteLine($"{order}. {product}");

        ++order;
      }
    }
    else
    {
      Console.WriteLine("The inventory is empty.");
    }
  }

  private async Task HandleEditChoice()
  {
    Console.WriteLine("Please enter the name of the product you want to edit.");

    string name = Utils.GetStringInput("Name").Capitalize();

    var product = await _inventory.GetProductByName(name);
    
    Utils.DisplayStars();

    if (product is null)
    {
      Console.WriteLine($"The product with the name {name} is not in the inventory.");

      return;
    }

    Console.WriteLine("Choose one of the following properties of a product to edit [1..3]");
    
    Utils.DisplayEditMenu();
    
    Utils.DisplayStars();

    string choice = Utils.GetStringInput("Choice");
    
    Utils.DisplayStars();

    bool edited = true;

    switch (choice)
    {
      case "1":
        string newName = Utils.GetStringInput("Name").Capitalize();

        product.Name = newName;
        
        break;
      case "2":
        decimal newPrice = Utils.GetPriceInput();

        product.Price = newPrice;
        
        break;
      case "3":
        int newQuantity = Utils.GetQuantityInput();

        product.Quantity = newQuantity;

        break;
      default:
        edited = false;

        break;
    }

    if (edited)
    {
      Utils.DisplayStars();

      try
      {
        await _inventory.UpdateProduct(product);
        
        Console.WriteLine("The product has been edited successfully.");
      }
      catch (ProductAlreadyExistsException)
      {
        Console.WriteLine("Another product with the same name exists in the inventory; no changes were made.");
      }
    }
    else
    {
      Console.WriteLine("The choice is not in the range [1..3], nothing has changed.");
    }
  }

  private async Task HandleDeleteChoice()
  {
    Console.WriteLine("Please enter the name of the product you want to delete.");
    
    string name = Utils.GetStringInput("Name").Capitalize();

    var product = await _inventory.GetProductByName(name);
    
    Utils.DisplayStars();

    if (product is null)
    {
      Console.WriteLine($"The product with the name {name} is not in the inventory.");
    }
    else
    {
      await _inventory.DeleteProduct(product);
      
      Console.WriteLine("The product has been deleted successfully.");
    }
  }

  private async Task HandleSearchChoice()
  {
    Console.WriteLine("Please enter the name of the product you are looking for.");
      
    string name = Utils.GetStringInput("Name").Capitalize();

    var product = await _inventory.GetProductByName(name);
      
    Utils.DisplayStars();

    Console.WriteLine(product is null
      ? $"The product with the name {name} is not in the inventory."
      : $"Here is the information of the product you are looking for:\n{product}");
  }
}