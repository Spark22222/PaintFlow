namespace PaintStore.Model.Models;

public class PaintStore
{
    public List<PaintProduct> Products { get; set; }

    public PaintStore(List<PaintProduct> products)
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