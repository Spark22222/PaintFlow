namespace PaintFlow.Models;

public class PaintStore
{
    public PaintProduct[] Products { get; set; }

    public PaintStore(PaintProduct[] products)
    {
        Products = products;
    }

    public void DisplayAvailableProducts()
    {
        Console.WriteLine("Available Paint Products");
        Console.WriteLine("========================");
        Console.WriteLine();

        foreach (PaintProduct product in Products)
        {
            product.DisplayInfo();
        }
    }
}