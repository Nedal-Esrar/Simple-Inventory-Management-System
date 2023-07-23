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
    if (_productInventory.Size == 0)
    {
      Console.WriteLine("The inventory is empty.");
    }
    else
    {
      int order = 1;
      
      foreach (var product in _productInventory)
      {
        Console.WriteLine($"{order}. {product}");

        ++order;
      }
    }
  }

  private void HandleEditChoice()
  {
    Console.WriteLine("Please enter the name of the product you want to edit.");

    string name = Utils.GetStringInput("Name");

    int productIndex = _productInventory.GetProductIndex(name);
    
    Utils.DisplayStars();

    if (productIndex == -1)
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
        string newName = Utils.GetStringInput("Name");
        
        _productInventory.SetProductNameAt(productIndex, newName);
        
        break;
      case "2":
        decimal newPrice = Utils.GetPriceInput();
        
        _productInventory.SetProductPriceAt(productIndex, newPrice);
        
        break;
      case "3":
        int newQuantity = Utils.GetQuantityInput();
        
        _productInventory.SetProductQuantityAt(productIndex, newQuantity);

        break;
      default:
        edited = false;

        break;
    }

    if (edited)
    {
      Utils.DisplayStars();
      
      Console.WriteLine("The product has been edited successfully.");
    }
    else
    {
      Console.WriteLine("The choice is not in the range [1..3], nothing has changed.");
    }
  }

  private void HandleDeleteChoice()
  {
    Console.WriteLine("Please enter the name of the product you want to delete.");
    
    string name = Utils.GetStringInput("Name");

    int productIndex = _productInventory.GetProductIndex(name);
    
    Utils.DisplayStars();

    if (productIndex == -1)
    {
      Console.WriteLine($"The product with the name {name} is not in the inventory.");
    }
    else
    {
      _productInventory.DeleteProductAt(productIndex);
      
      Console.WriteLine("The product has been deleted successfully.");
    }
  }

  private void HandleSearchChoice()
  {
    throw new NotImplementedException();
  }
}