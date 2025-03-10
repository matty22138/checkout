namespace Checkout;

public interface IBasket
{
    public void AddItem(Item item);
    public decimal GetTotal();
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

    public decimal GetTotal()
    {
        var totalPrice = 0m;

        foreach (var itemGroup in _basketItems)
        {
            var item = itemGroup.Key;
            var itemQuantity = itemGroup.Value;
            var itemDiscount = item.Discount;

            if (itemDiscount != null)
            {
                totalPrice += CalculateDiscountedTotal(itemQuantity, item);
            }
            else
            {
                totalPrice += CalculateRegularTotal(itemQuantity, item);
            }
        }

        return totalPrice;
    }

    private static decimal CalculateDiscountedTotal(int itemQuantity, Item item)
    {
        var itemDiscount = item.Discount;
        var total = itemQuantity / itemDiscount.Threshold * itemDiscount.Price;
        var remainingItems = itemQuantity % itemDiscount.Threshold;
        total += item.Price * remainingItems;
        return total;
    }

    private static decimal CalculateRegularTotal(int itemQuantity, Item item)
    {
        return item.Price * itemQuantity;
    }
}