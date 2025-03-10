public class Discount
{
    public int QuantityThreshold { get; }
    public decimal DiscountedPrice { get; }

    public Discount(int quantityThreshold, decimal discountedPrice)
    {
        QuantityThreshold = quantityThreshold;
        DiscountedPrice = discountedPrice;
    }
}