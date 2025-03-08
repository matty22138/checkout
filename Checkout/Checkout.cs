namespace Checkout;

public class Checkout
{
    private readonly List<string> _scannedItems = new List<string>();

    public void Scan(string sku)
    {
        _scannedItems.Add(sku);
    }

    public decimal GetTotalPrice()
    {
        if (_scannedItems.Any())
        {
            if (_scannedItems.First() == "B")
            {
                return 30m;
            }

            return 50m;
        }

        return 0;
    }
}
