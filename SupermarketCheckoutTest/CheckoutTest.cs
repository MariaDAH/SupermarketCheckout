using SupermarketCheckout;

namespace SupermarketCheckoutTest;

public class CheckoutTest
{
  
  private ICheckout _checkout;
  private Cart? _cart;
  private List<Item> _items;
  
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
    var sut = new Checkout(_cart);
    
    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => sut.Prices = null);
    Assert.That(exception.ParamName, Is.EqualTo("value"));
  }
  
  [Test]
  public void Prices_Get_ShouldReturnSetValue()
  {
    //Arrange
    var sut = new Checkout(_cart);
    List<Price> prices = [
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
    var sut = new Checkout(_cart);
    
    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => sut.Cart = null);
    Assert.That(exception.ParamName, Is.EqualTo("value"));
  }

  [Test]
  public void Scan_InvalidItemCode_ShouldThrowInvalidSKUException()
  {
    //Arrange 
    var sut = new Checkout(_cart);
    _items.Add(Item.Create("NoExists"));
    var item = _items[0].SKU;
    
    //Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => sut.Scan(item));
    Assert.That(exception.Message, Is.EqualTo($"{item} is not a valid SKU"));
  }
}