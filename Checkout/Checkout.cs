﻿namespace Checkout;

public class Checkout
{
    private readonly IEnumerable<Item> _availableItems = new List<Item>();
    private readonly IList<string> _scannedSkus = new List<string>();

    public Checkout(IEnumerable<Item> availableItems)
    {
        _availableItems = availableItems;
    }

    public void Scan(string sku)
    {
        _scannedSkus.Add(sku);
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
