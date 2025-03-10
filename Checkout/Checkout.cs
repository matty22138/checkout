﻿namespace Checkout;

public class Checkout
{
    private readonly IEnumerable<Item> _itemsOnSale = new List<Item>();
    private readonly IDictionary<Item, int> _scannedItems = new Dictionary<Item, int>();

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

        if (_scannedItems.ContainsKey(scannedItem))
        {
            _scannedItems[scannedItem] += 1;
        }
        else
        {
            _scannedItems[scannedItem] = 1;
        }
    }

    public decimal GetTotalPrice()
    {
        var totalPrice = 0m;

        foreach (var itemGroup in _scannedItems)
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
