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

public class Checkout
{
    private readonly IEnumerable<Item> _availableItems = new List<Item>();
    private readonly IList<string> _scannedItems = new List<string>();

    public Checkout(IEnumerable<Item> availableItems)
    {
        _availableItems = availableItems;
    }

    public void Scan(string sku)
    {
        _scannedItems.Add(sku);
    }

    public decimal GetTotalPrice()
    {
        var totalPrice = 0m;

        if (_scannedItems.Any())
        {
            foreach (var sku in _scannedItems)
            {
                var scannedItem = _availableItems.First((i) => i.Sku == sku);
                totalPrice += scannedItem.Price;
            }
            return totalPrice;
        }

        return 0;
    }
}
