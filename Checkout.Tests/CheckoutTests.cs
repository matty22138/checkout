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
        _checkout = new Checkout([_itemA, _itemB, _itemC, _itemD]);
    }

    [Test]
    public void GetTotalPrice_WhenNoItemsScanned_ReturnsZero()
    {
        var total = _checkout.GetTotalPrice();

        Assert.That(total, Is.Zero);
    }

    [TestCase("A", 50)]
    [TestCase("B", 30)]
    [TestCase("C", 20)]
    [TestCase("D", 15)]
    public void Scan_WithASingleItem_AddsItemToTheCheckout(string itemSku, decimal itemPrice)
    {
        _checkout.Scan(itemSku);

        var total = _checkout.GetTotalPrice();
        Assert.That(total, Is.EqualTo(itemPrice));
    }
}
