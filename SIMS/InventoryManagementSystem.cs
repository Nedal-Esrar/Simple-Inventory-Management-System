using Utils = SIMS.Utilites;

namespace SIMS;

public class InventoryManagementSystem
{
  private Inventory _productInventory;

  public InventoryManagementSystem()
  {
    _productInventory = new();
  }
  
  public void Run()
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

      exit = HandleChoice(choice);
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
  private bool HandleChoice(string choice)
  {
    switch (choice)
    {
      case "1":
        HandleAddChoice();
        
        break;
      case "2":
        HandleViewAllChoice();
        
        break;
      case "3":
        HandleEditChoice();
        
        break;
      case "4":
        HandleDeleteChoice();
        
        break;
      case "5":
        HandleSearchChoice();
        
        break;
      case "6":
        Console.WriteLine("Thank you for using Simple Inventory Management System ;)");

        return true;
      default:
        Console.WriteLine("the choice is not in the range [1..6]");
        
        break;
    }

    return false;
  }

  private void HandleAddChoice()
  {
    Console.WriteLine("Please enter the information of the product you want to add.");

    string name = Utils.GetStringInput("Name");

    decimal price = Utils.GetPriceInput();

    int quantity = Utils.GetQuantityInput();

    bool success = _productInventory.AddProduct(name, price, quantity);
    
    Utils.DisplayStars();

    if (success)
    {
      Console.WriteLine("The product has been added successfully.");
    }
    else
    {
      Console.WriteLine("A product with the same name exists in the inventory; no changes were made.");
    }
  }

  private void HandleViewAllChoice()
  {
    throw new NotImplementedException();
  }

  private void HandleEditChoice()
  {
    throw new NotImplementedException();
  }

  private void HandleDeleteChoice()
  {
    throw new NotImplementedException();
  }

  private void HandleSearchChoice()
  {
    throw new NotImplementedException();
  }
}