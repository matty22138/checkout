namespace Checkout;

public interface Item
{
    public char Sku { get; }
    public decimal Price { get; }
    public int? DiscountThreshold { get; }
    public decimal? DiscountPrice { get; }
}

public class ItemA : Item
{
    public char Sku { get { return 'A'; } }
    public decimal Price { get { return 50m; } }
    public int? DiscountThreshold => 3;
    public decimal? DiscountPrice => 130m;
}

public class ItemB : Item
{
    public char Sku { get { return 'B'; } }
    public decimal Price { get { return 30m; } }

    public int? DiscountThreshold => 2;

    public decimal? DiscountPrice => 45m;
}

public class ItemC : Item
{
    public char Sku { get { return 'C'; } }
    public decimal Price { get { return 20m; } }
    public int? DiscountThreshold => null;
    public decimal? DiscountPrice => null;
}

public class ItemD : Item
{
    public char Sku { get { return 'D'; } }
    public decimal Price { get { return 15m; } }
    public int? DiscountThreshold => null;
    public decimal? DiscountPrice => null;
}