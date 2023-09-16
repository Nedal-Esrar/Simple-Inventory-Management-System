namespace SIMS.Repositories.SqlRepo;

public static class SqlCommands
{
  public const string GetAllProducts =
    """
    SELECT *
    FROM products
    """;

  public const string GetProductByName =
    """
    SELECT *
    FROM products
    WHERE name = @name
    """;

  public const string AddProduct =
    """
    INSERT INTO products
    VALUES (@name, @price, @quantity)
    """;

  public const string UpdateProduct =
    """
    UPDATE products
    SET name = @name,
        price = @price,
        quantity = @quantity
    WHERE id = @id
    """;

  public const string DeleteProduct =
    """
    DELETE FROM products
    WHERE id = @id
    """;
}