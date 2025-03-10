namespace Checkout;

public class Item
{
    public char Sku { get; }
    public decimal Price { get; }
    public Discount Discount { get; }

    public Item(char sku, decimal price, Discount discount = null)
    {
        Sku = sku;
        Price = price;
        Discount = discount;
    }
}