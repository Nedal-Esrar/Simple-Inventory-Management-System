using System.Collections;

namespace SIMS;

public class Inventory : IEnumerable<IReadOnlyProduct>
{
  IList<Product> _productList;

  public Inventory()
  {
    _productList = new List<Product>();
  }

  public int Size => _productList.Count;
  
  /// <summary>
  /// Retrieves the index of a product with the specified name in the inventory.
  /// Case-insensitive search is performed to match the product name.
  /// </summary>
  /// <param name="productName"></param>
  /// <returns>
  /// The index of the product if found; otherwise, returns -1 to indicate that
  /// the product with the given name was not found in the inventory.
  /// </returns>
  public int GetProductIndex(string productName)
  {
    int productListSize = Size;
    
    for (int i = 0; i < productListSize; ++i)
    {
      if (_productList[i].Name.Equals(productName, StringComparison.OrdinalIgnoreCase))
      {
        return i;
      }
    }

    return -1;
  }

  /// <summary>
  /// Adds a new product to the inventory if a product
  /// with the same name does not already exist. 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="price"></param>
  /// <param name="quantity"></param>
  /// <returns>
  /// True if the product was successfully added to the inventory;
  /// False if a product with the same name already exists,
  /// and the product was not added.
  /// </returns>
  public bool AddProduct(string name, decimal price, int quantity)
  {
    if (GetProductIndex(name) != -1)
    {
      return false;
    }

    Product productToAdd = new Product(name, price, quantity);
    
    _productList.Add(productToAdd);

    return true;
  }

  public void DeleteProductAt(int index)
  {
    if (index < 0 || index >= Size)
    {
      throw new IndexOutOfRangeException("Provide A valid index :|");
    }
    
    _productList.RemoveAt(index);
  }

  public void SetProductNameAt(int index, string name)
  {
    if (index < 0 || index >= Size)
    {
      throw new IndexOutOfRangeException("Provide A valid index :|");
    }
    
    _productList[index].Name = name;
  }

  public void SetProductPriceAt(int index, decimal price)
  {
    if (index < 0 || index >= Size)
    {
      throw new IndexOutOfRangeException("Provide A valid index :|");
    }
    
    _productList[index].Price = price;
  }

  public void SetProductQuantityAt(int index, int quantity)
  {
    if (index < 0 || index >= Size)
    {
      throw new IndexOutOfRangeException("Provide A valid index :|");
    }
    
    _productList[index].Quantity = quantity;
  }

  public IReadOnlyProduct GetProductAt(int index)
  {
    if (index < 0 || index >= Size)
    {
      throw new IndexOutOfRangeException("Provide A valid index :|");
    }

    return _productList[index];
  }

  public IEnumerator<IReadOnlyProduct> GetEnumerator()
  {
    foreach (IReadOnlyProduct product in _productList)
    {
      yield return product;
    }
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}