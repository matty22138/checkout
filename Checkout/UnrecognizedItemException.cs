public class UnrecognizedItemException : Exception
{
    public UnrecognizedItemException(char sku) : base($"Cannot recognize item {sku}.") { }
}