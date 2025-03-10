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
    private readonly IDictionary<Item, int> _scannedItems2 = new Dictionary<Item, int>();

    public Checkout(IEnumerable<Item> availableItems, IEnumerable<Discount> discounts)
    {
        _itemsOnSale = availableItems;
        _discounts = discounts;
    }

    public void Scan(char sku)
    {
        var scannedItem = _itemsOnSale.FirstOrDefault((i) => i.Sku == sku);

        if (scannedItem == null)
        {
            throw new UnrecognizedItemException(sku);
        }

        if (_scannedItems2.ContainsKey(scannedItem))
        {
            _scannedItems2[scannedItem] += 1;
        }
        else
        {
            _scannedItems2[scannedItem] = 1;
        }

        _scannedItems.Add(scannedItem);
    }

    public decimal GetTotalPrice()
    {
        var totalPrice = 0m;

        foreach (var itemGroup in _scannedItems2)
        {
            var scannedItem = itemGroup.Key;
            var scannedItemQuantity = itemGroup.Value;

            if (scannedItem.DiscountThreshold != null && scannedItemQuantity >= scannedItem.DiscountThreshold)
            {
                totalPrice += (scannedItemQuantity / (int)scannedItem.DiscountThreshold) * (decimal)scannedItem.DiscountPrice;
                var remainingItems = scannedItemQuantity % (int)scannedItem.DiscountThreshold;
                totalPrice += scannedItem.Price * remainingItems;
            }
            else
            {
                totalPrice += scannedItem.Price * scannedItemQuantity;
            }
        }

        return totalPrice;
    }
}
