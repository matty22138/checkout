namespace Checkout;

public interface Item
{
    public string Sku { get; }
    public decimal Price { get; }
}

public class ItemA : Item
{
    public string Sku { get { return "A"; } }
    public decimal Price { get { return 50m; } }
}

public class ItemB : Item
{
    public string Sku { get { return "B"; } }
    public decimal Price { get { return 30m; } }
}

public class ItemC : Item
{
    public string Sku { get { return "C"; } }
    public decimal Price { get { return 20m; } }
}

public class ItemD : Item
{
    public string Sku { get { return "D"; } }
    public decimal Price { get { return 15m; } }
}