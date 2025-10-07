namespace SupermarketCheckout;

public class Checkout(Cart cart, IStrategy strategy) : ICheckout
{
  private IStrategy _strategy = strategy;
  
  private List<Price> _prices = [
    Price.Create("A", 50, 3, 130),
    Price.Create("B", 30, 2, 45),
    Price.Create("C", 20, null, null),
    Price.Create("D", 15, null, null)
  ];

  public List<Price> Prices
  {
    get => _prices;
    set => _prices = value ?? throw new ArgumentNullException(nameof(value));
  }

  private Cart _cart = cart;
  
  public Cart Cart
  {
    get => _cart;
    set => _cart = value ?? throw new ArgumentNullException(nameof(value));
  }
  
  public void Scan(string item)
  {
    if (!_prices.Exists(x => x.SKU == item))
      throw new ArgumentException($"{item} is not a valid SKU");

    var existInCart = _cart.Items.Exists(x => x.SKU == item);
    if (existInCart)
    {
      _cart!.Items.FirstOrDefault(x => x.SKU == item)!.Quantity++;
    }
    else
    {
      _cart!.Items.Add(Item.Create(item));
    }
  }

  public int GetTotalPrice()
  {
    throw new NotImplementedException();
  }
}