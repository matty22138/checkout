namespace Checkout;

public class Checkout
{
    private readonly IEnumerable<Item> _itemsOnSale;
    private readonly IBasket _basket;

    public Checkout(IEnumerable<Item> availableItems, IBasket basket)
    {
        _itemsOnSale = availableItems;
        _basket = basket;
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
