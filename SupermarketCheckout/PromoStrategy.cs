namespace SupermarketCheckout;

public class PromoStrategy(Item item, List<Price> prices): IStrategy
{

  public int CalculatePrice()
  {
    var price = prices.Find(x => x.SKU == item.SKU);

    var originalQuantity = item.Quantity;
    var quantityPromotion = price.QuantityPromotion;
    var pricePromotion = price.PricePromotion;
    var groupByPromo = originalQuantity / quantityPromotion;
    var mod = originalQuantity % quantityPromotion;
    var promo = groupByPromo * pricePromotion;
    promo += (mod * price.UnitPrice);
    return promo ?? 0;
  }
}