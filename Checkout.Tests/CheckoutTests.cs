namespace Checkout.Tests;

public class CheckoutTests
{
    private readonly Item _itemA = new ItemA();
    private readonly Item _itemB = new ItemB();
    private Checkout _checkout;


    [SetUp]
    public void Setup()
    {
        _checkout = new Checkout([_itemA, _itemB]);
    }

    [Test]
    public void GetTotalPrice_WhenNoItemsScanned_ReturnsZero()
    {
        var total = _checkout.GetTotalPrice();

        Assert.That(total, Is.Zero);
    }

    [Test]
    public void Scan_WithASingleItemA_AddsItemAToTheCheckout()
    {
        _checkout.Scan("A");

        var total = _checkout.GetTotalPrice();
        Assert.That(total, Is.EqualTo(_itemA.Price));
    }

    [Test]
    public void Scan_WithASingleItemB_AddsItemBToTheCheckout()
    {
        _checkout.Scan("B");

        var total = _checkout.GetTotalPrice();
        Assert.That(total, Is.EqualTo(_itemB.Price));
    }
}
