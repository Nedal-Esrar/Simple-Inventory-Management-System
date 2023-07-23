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

  private bool HandleChoice(string choice)
  {
    throw new NotImplementedException();
  }
}