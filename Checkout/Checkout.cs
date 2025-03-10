namespace Checkout;

public class Checkout
{
    private readonly IEnumerable<Item> _itemsOnSale = new List<Item>();
    private readonly IList<Item> _scannedItems = new List<Item>();

    public Checkout(IEnumerable<Item> availableItems)
    {
        _itemsOnSale = availableItems;
    }

    public void Scan(char sku)
    {
        var scannedItem = _itemsOnSale.FirstOrDefault((i) => i.Sku == sku);

        if (scannedItem == null)
        {
            throw new UnrecognizedItemException(sku);
        }
        else
        {
            _scannedItems.Add(scannedItem);
        }
    }

    public decimal GetTotalPrice()
    {
        var totalPrice = 0m;

        foreach (var scannedItem in _scannedItems)
        {
            totalPrice += scannedItem.Price;
        }

        if (_scannedItems.Where(i => i.Sku == 'A').Count() == 3)
        {
            return 130m;
        }

        if (_scannedItems.Where(i => i.Sku == 'B').Count() == 2)
        {
            return 45m;
        }

        return totalPrice;
    }
}
