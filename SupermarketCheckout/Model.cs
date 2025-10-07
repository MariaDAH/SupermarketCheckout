namespace SupermarketCheckout;

public record Item
{
  public string SKU { get; set; }

  public int Quantity { get; set; } = 1;

  public static Item Create(string sku)
  {
    return new Item
    {
      SKU = sku
    };
  }

  public void Update(int quantity)
  {
    Quantity = quantity;
  }
}

public record Price
{
  public string SKU { get; set; }
  public int UnitPrice { get; set; }
  public int? QuantityPromotion { get; set; } = 0;
  public int? PricePromotion { get; set; } = 0;

  public static Price Create(string sku, int price, int? quantityPromotion, int? pricePromotion)
  {
    return new Price
    {
      SKU = sku,
      UnitPrice = price,
      QuantityPromotion = quantityPromotion,
      PricePromotion = pricePromotion
    };
  }

  public void Update(int unitPrice, int quantityPromotion, int pricePromotion)
  {
    UnitPrice = unitPrice;
    QuantityPromotion = quantityPromotion;
    PricePromotion = pricePromotion;
  }
}


public record Cart
{
  public List<Item> Items { get; set; } = new();
  public int TotalAmount { get; set; }
  public int TotalDiscount { get; set; }

  public static Cart Create(int totalAmount, int totalDiscount)
  {
    return new Cart
    {
      TotalAmount = totalAmount,
      TotalDiscount = totalDiscount,
    };
  }
}