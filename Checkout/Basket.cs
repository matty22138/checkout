namespace Checkout;

public interface IBasket
{
    public void AddItem(Item item);
    public decimal GetItemsTotal();
}

public class Basket : IBasket
{
    private readonly IDictionary<Item, int> _basketItems = new Dictionary<Item, int>();

    public void AddItem(Item item)
    {
        if (_basketItems.ContainsKey(item))
        {
            _basketItems[item] += 1;
        }
        else
        {
            _basketItems[item] = 1;
        }
    }

    public decimal GetItemsTotal()
    {
        var totalPrice = 0m;

        foreach (var itemGroup in _basketItems)
        {
            var item = itemGroup.Key;
            var itemQuantity = itemGroup.Value;
            var itemDiscount = item.Discount;

            if (itemDiscount != null)
            {
                totalPrice += itemQuantity / itemDiscount.QuantityThreshold * itemDiscount.DiscountedPrice;
                var remainingItems = itemQuantity % itemDiscount.QuantityThreshold;
                totalPrice += item.Price * remainingItems;
            }
            else
            {
                totalPrice += item.Price * itemQuantity;
            }
        }

        return totalPrice;
    }
}