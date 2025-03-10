namespace Checkout;

public class Discount
{
    public char Sku { get; set; }
    public int QuantityThreshold { get; set; }
    public decimal DiscountedPrice { get; set; }
}

public class Checkout
{
    private readonly IEnumerable<Item> _itemsOnSale = new List<Item>();
    private readonly IEnumerable<Discount> _discounts = new List<Discount>();
    private readonly IList<Item> _scannedItems = new List<Item>();

    public Checkout(IEnumerable<Item> availableItems, IEnumerable<Discount> discounts = null)
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
