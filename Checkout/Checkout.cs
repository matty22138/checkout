namespace Checkout;

public class Checkout
{
    private readonly IEnumerable<Item> _availableItems = new List<Item>();
    private readonly IList<char> _scannedSkus = new List<char>();
    private readonly IList<Item> _scannedItems = new List<Item>();

    public Checkout(IEnumerable<Item> availableItems)
    {
        _availableItems = availableItems;
    }

    public void Scan(char sku)
    {
        var scannedItem = _availableItems.FirstOrDefault((i) => i.Sku == sku);

        if (scannedItem == null)
        {
            throw new UnrecognizedItemException(sku);
        }
        else
        {
            _scannedSkus.Add(sku);
        }
    }

    public decimal GetTotalPrice()
    {
        var totalPrice = 0m;

        foreach (var sku in _scannedSkus)
        {
            var scannedItem = _availableItems.First((i) => i.Sku == sku);
            totalPrice += scannedItem.Price;
        }

        return totalPrice;
    }
}
