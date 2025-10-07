using SupermarketCheckout;

namespace SupermarketCheckoutTest;

public class CheckoutTest
{
  
  private Cart _cart;
  private List<Item> _items;
  private IStrategy? _strategy;
  
  [SetUp]
  public void Setup()
  {
    _items = new List<Item>();
    _cart = new Cart();
  }
  
  [Test]
  public void Prices_SetNull_ShouldThrowArgumentNullException()
  {
    //Arrange
    var sut = new Checkout(_cart, _strategy);
    
    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => sut.Prices = null);
    Assert.That(exception.ParamName, Is.EqualTo("value"));
  }
  
  [Test]
  public void Prices_Get_ShouldReturnSetValue()
  {
    //Arrange
    var sut = new Checkout(_cart, _strategy);
    List<Price> prices =
    [
      Price.Create("A", 50, 3, 130),
      Price.Create("B", 30, 2, 45),
      Price.Create("C", 20, null, null),
      Price.Create("D", 15, null, null)
    ];
    sut.Prices = prices;
    
    // Act & Assert
    var result = sut.Prices;
    Assert.That(prices, Is.SameAs(result));
  }
  
  [Test]
  public void Checkout_SetNullCart_ShouldThrowArgumentNullException()
  {
    //Arrange
    var sut = new Checkout(_cart, _strategy);
    
    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => sut.Cart = null);
    Assert.That(exception.ParamName, Is.EqualTo("value"));
  }

  [Test]
  public void Scan_InvalidItemCode_ShouldThrowInvalidSKUException()
  {
    //Arrange 
    var sut = new Checkout(_cart, _strategy);
    _items.Add(Item.Create("NoExists"));
    var item = _items[0].SKU;
    
    //Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => sut.Scan(item));
    Assert.That(exception.Message, Is.EqualTo($"{item} is not a valid SKU"));
  }
  
  [Test]
  public void ScanItem_ExistsInCart_ShouldIncreaseQuantityByOne()
  {
    //Arrange 
    var sut = new Checkout(_cart, _strategy);
    _items.Add(Item.Create("A"));
    _items.Add(Item.Create("A"));
    
    
    //Act & Assert
    _items.ForEach(x => sut.Scan(x.SKU));
    var cartItem = _cart.Items.FirstOrDefault(x => x.SKU == "A");
    Assert.That(cartItem!.Quantity, Is.EqualTo(2));
  }
  
  [Test]
  public void ScanItem_DoesNotExistInCart_ShouldAddItemToCart()
  {
    //Arrange 
    var sut = new Checkout(_cart, _strategy);
    _items.Add(Item.Create("A"));
    var result = _items[0];
    
    //Act & Assert
    _items.ForEach(x => sut.Scan(x.SKU));
    Assert.That(_items[0], Is.EqualTo(result));
  }
  
  [Test]
  public void CalculatePriceWhenPriceInList__UsingIndividualStrategy_ShouldReturnPrice()
  {
    //Arrange 
    var sut = new Checkout(_cart, _strategy);
    List<Price> prices =
    [
      Price.Create("A", 50, 3, 130),
      Price.Create("B", 30, 2, 45),
      Price.Create("C", 20, null, null),
      Price.Create("D", 15, null, null)
    ];
    sut.Prices = prices;
    _items.Add(Item.Create("A"));
    _items.ForEach(x => sut.Scan(x.SKU));
    var strategy = new IndividualStrategy(_cart.Items[0], prices);
    
    //Act & Assert
    Assert.That(strategy.CalculatePrice(), Is.EqualTo(50));
  }
  
  [Test]
  public void CalculatePriceWhenPriceInList__UsingPromoStrategy_ShouldReturnPrice()
  {
    //Arrange 
    var sut = new Checkout(_cart, _strategy);
    List<Price> prices =
    [
      Price.Create("A", 50, 3, 130),
      Price.Create("B", 30, 2, 45),
      Price.Create("C", 20, null, null),
      Price.Create("D", 15, null, null)
    ];
    sut.Prices = prices;
    _items.Add(Item.Create("B"));
    _items.Add(Item.Create("B"));
    _items.Add(Item.Create("B"));
    _items.ForEach(x => sut.Scan(x.SKU));
    var strategy = new PromoStrategy(_cart.Items[0], prices);
    
    //Act & Assert
    Assert.That(strategy.CalculatePrice(), Is.EqualTo(75));
  }
  
  [Test]
  public void Calculate_TotalPrice_ShouldReturnTotalPrice()
  {
    //Arrange 
    var sut = new Checkout(_cart, _strategy);
    List<Price> prices =
    [
      Price.Create("A", 50, 3, 130),
      Price.Create("B", 30, 2, 45),
      Price.Create("C", 20, null, null),
      Price.Create("D", 15, null, null)
    ];
    sut.Prices = prices;
    _items.Add(Item.Create("B"));
    _items.Add(Item.Create("B"));
    _items.Add(Item.Create("B"));
    _items.Add(Item.Create("A"));
    _items.Add(Item.Create("C"));
    _items.ForEach(x => sut.Scan(x.SKU));
    
    //Act & Assert
    Assert.That(sut.GetTotalPrice(), Is.EqualTo(145));
  }
  
}
