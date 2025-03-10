public class Discount
{
    public int Threshold { get; }
    public decimal Price { get; }

    public Discount(int threshold, decimal price)
    {
        Threshold = threshold;
        Price = price;
    }
}