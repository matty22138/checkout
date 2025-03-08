namespace Checkout.Tests;

public class CheckoutTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var checkout = new Checkout();
        Assert.That(checkout.Foo(), Is.EqualTo(1));
    }
}
