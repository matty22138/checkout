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
}
