namespace Checkout;

public class Basket
{
    private readonly IDictionary<Item, int> _items = new Dictionary<Item, int>();

    public void AddItem(Item item)
    {
        if (_items.ContainsKey(item))
        {
            _items[item] += 1;
        }
        else
        {
            _items[item] = 1;
        }
    }

    public decimal GetItemsTotal()
    {
        var totalPrice = 0m;

        foreach (var itemGroup in _items)
        {
            var scannedItem = itemGroup.Key;
            var scannedItemQuantity = itemGroup.Value;

            if (scannedItem.Discount != null && scannedItemQuantity >= scannedItem.Discount.QuantityThreshold)
            {
                totalPrice += scannedItemQuantity / scannedItem.Discount.QuantityThreshold * scannedItem.Discount.DiscountedPrice;

                var remainingItems = scannedItemQuantity % scannedItem.Discount.QuantityThreshold;
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

public class Checkout
{
    private readonly IEnumerable<Item> _itemsOnSale;
    private readonly IDictionary<Item, int> _scannedItems = new Dictionary<Item, int>();
    private readonly Basket _basket = new Basket();

    public Checkout(IEnumerable<Item> availableItems)
    {
        _itemsOnSale = availableItems;
    }

    public void Scan(char sku)
    {
        var scannedItem = _itemsOnSale.FirstOrDefault((i) => i.Sku == sku) ?? throw new UnrecognizedItemException(sku);

        _basket.AddItem(scannedItem);
    }

    public decimal GetTotalPrice()
    {
        return _basket.GetItemsTotal();
    }
}
