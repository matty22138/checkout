namespace Checkout.Tests;

public class CheckoutTests
{
    private readonly Item _itemA = new ItemA();
    private readonly Item _itemB = new ItemB();
    private readonly Item _itemC = new ItemC();
    private readonly Item _itemD = new ItemD();
    private Checkout _checkout;

    [SetUp]
    public void Setup()
    {
        _checkout = new Checkout(
            [_itemA, _itemB, _itemC, _itemD],
            [
                new Discount { Sku = 'A', DiscountedPrice = 130m, QuantityThreshold = 3 },
                new Discount { Sku = 'B', DiscountedPrice = 45m, QuantityThreshold = 2 }
            ]);
    }

    [Test]
    public void GetTotalPrice_WhenNoItemsScanned_ReturnsZero()
    {
        var total = _checkout.GetTotalPrice();

        Assert.That(total, Is.Zero);
    }

    [TestCase('A', 50)]
    [TestCase('B', 30)]
    [TestCase('C', 20)]
    [TestCase('D', 15)]
    public void Scan_WithASingleItem_AddsItemToTheCheckout(char itemSku, decimal itemPrice)
    {
        _checkout.Scan(itemSku);

        var total = _checkout.GetTotalPrice();
        Assert.That(total, Is.EqualTo(itemPrice));
    }

    [Test]
    public void Scan_WithMultipleOfDifferentItems_AddsMultipleItemsAndTotalsTheirPrices()
    {
        _checkout.Scan(_itemA.Sku);
        _checkout.Scan(_itemB.Sku);

        var total = _checkout.GetTotalPrice();
        Assert.That(total, Is.EqualTo(80m));
    }

    [Test]
    public void Scan_WithMultipleOfTheSameItems_AddsMultipleItemsAndTotalsTheirPrices()
    {
        _checkout.Scan(_itemA.Sku);
        _checkout.Scan(_itemA.Sku);

        var total = _checkout.GetTotalPrice();
        Assert.That(total, Is.EqualTo(100m));
    }

    [Test]
    public void Scan_WithUnrecognizedItem_ThrowsErrorIndicatingThatItemIsUnrecognized()
    {
        var ex = Assert.Throws<UnrecognizedItemException>(() => _checkout.Scan('Y'));
        Assert.That(ex.Message, Is.EqualTo("Cannot recognize item Y."));
    }

    [Test]
    public void Scan_WhenSpecialOfferIsMetForItemA_AppliesItemADiscountToTheTotal()
    {
        _checkout.Scan(_itemA.Sku);
        _checkout.Scan(_itemA.Sku);
        _checkout.Scan(_itemA.Sku);

        var total = _checkout.GetTotalPrice();
        Assert.That(total, Is.EqualTo(130m));
    }

    [Test]
    public void Scan_WhenSpecialOfferIsMetForItemB_AppliesItemBDiscountToTheTotal()
    {
        _checkout.Scan(_itemB.Sku);
        _checkout.Scan(_itemB.Sku);

        var total = _checkout.GetTotalPrice();
        Assert.That(total, Is.EqualTo(45m));
    }

    [Test]
    public void Scan_WhenMultipleDiscountsCanBeApplied_AppliesMultipleDiscountsToTheTotal()
    {
        _checkout.Scan(_itemB.Sku);
        _checkout.Scan(_itemB.Sku);//45m
        _checkout.Scan(_itemB.Sku);
        _checkout.Scan(_itemB.Sku);//90m
        _checkout.Scan(_itemA.Sku);
        _checkout.Scan(_itemA.Sku);
        _checkout.Scan(_itemA.Sku);//220m
        _checkout.Scan(_itemA.Sku);//270m
        _checkout.Scan(_itemC.Sku);//290m

        var total = _checkout.GetTotalPrice();
        Assert.That(total, Is.EqualTo(290m));
    }
}
