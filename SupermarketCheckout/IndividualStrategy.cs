namespace SupermarketCheckout;

public class IndividualStrategy(Item item, List<Price> prices) : IStrategy
{
  public int CalculatePrice()
  {
    var price = prices.Find(x => x.SKU == item.SKU);
    return item.Quantity * price.UnitPrice;
  }
}