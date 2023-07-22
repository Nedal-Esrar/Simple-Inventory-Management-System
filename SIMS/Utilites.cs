namespace SIMS;

public static class Utilites
{
  public static string Capitalize(this string str)
  {
    if (string.IsNullOrWhiteSpace(str))
    {
      return str;
    }

    return char.ToUpper(str[0]) + str.Substring(1).ToLower();
  }
  
  /// <summary>
  /// Prompts the user to input a non-empty string and loops until a valid string input is provided.
  /// A valid string is a string that contains at least one non-whitespace character.
  /// </summary>
  /// <param name="prompt">The prompt displayed to the user</param>
  /// <returns>The valid string input</returns>
  public static string GetStringInput(string prompt)
  {
    string name;

    do
    {
      Console.Write($"{prompt}: ");
      name = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(name));

    return name.Trim();
  }
  
  /// <summary>
  /// Prompts the user to input a price and loops until a valid price input is provided.
  /// A valid price is a price that is a decimal that is greater than zero.
  /// </summary>
  /// <returns>The valid price input</returns>
  public static decimal GetPriceInput()
  {
    string input;

    decimal price;
    
    do
    {
      Console.Write("Price: ");
      input = Console.ReadLine();
    } while (!decimal.TryParse(input, out price) || price <= 0);

    return price;
  }
  
  /// <summary>
  /// Prompts the user to input a quantity and Loops until a valid quantity input is provided.
  /// A valid quantity is a quantity that is an integer and greater than zero.
  /// </summary>
  /// <returns>The valid quantity input</returns>
  public static int GetQuantityInput()
  {
    string input;

    int quantity;
    
    do
    {
      Console.Write("Quantity: ");
      input = Console.ReadLine();
    } while (!int.TryParse(input, out quantity) || quantity < 1);

    return quantity;
  }
  
  public static void DisplayStars()
  {
    Console.WriteLine("***********************************************************************************");
  }
  
  public static void DisplayChoicesMenu()
  {
    Console.WriteLine("1. Add a product");
    Console.WriteLine("2. View all products");
    Console.WriteLine("3. Edit a product");
    Console.WriteLine("4. Delete a product");
    Console.WriteLine("5. Search for a product");
    Console.WriteLine("6. Exit");
  }
}