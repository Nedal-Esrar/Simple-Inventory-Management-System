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
}