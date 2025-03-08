namespace Checkout.Tests;

public class CheckoutTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetTotalPrice_WhenNoItemsScanned_ReturnsZero()
    {
        var checkout = new Checkout();

        var total = checkout.GetTotalPrice();

        Assert.That(total, Is.Zero);
    }

    [Test]
    public void Scan_WithASingleItemA_AddsItemToTheCheckout()
    {
        var checkout = new Checkout();

        checkout.Scan("A");

        var total = checkout.GetTotalPrice();
        Assert.That(total, Is.EqualTo(50m));
    }

    [Test]
    public void Scan_WithASingleItemB_AddsItemToTheCheckout()
    {
        var checkout = new Checkout();

        checkout.Scan("B");

        var total = checkout.GetTotalPrice();
        Assert.That(total, Is.EqualTo(30m));
    }
}
