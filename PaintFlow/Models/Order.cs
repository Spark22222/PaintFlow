namespace PaintFlow.Models;

public class Order
{
    public DateTime CreatedAt { get; }

    public PaintProduct[] Products { get; set; }

    public int[] Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public Order(PaintProduct[] product, int[] quantity)
    {
        CreatedAt = DateTime.Now;
        Products = product;
        Quantity = quantity;
        TotalPrice = GetTotalOrderPrice();
    }

    public void DisplayOrder()
    {
        Console.WriteLine("Order Details");
        Console.WriteLine("-------------");
        Console.WriteLine($"Created At: {CreatedAt}");
        Console.WriteLine();
        
        for (int i = 0; i < Products.Length; i++)
        {
            Console.WriteLine($"Product: {Products[i].Name}");
            Console.WriteLine($"Quantity: {Quantity[i]}");
            Console.WriteLine($"Unit Final Price: ${Products[i].GetFinalPrice():F2}");
            Console.WriteLine($"Subtotal: ${Products[i].GetFinalPrice() * Quantity[i]:F2}");
            Console.WriteLine();
        }
        
        Console.WriteLine($"Total Order Price: ${TotalPrice:F2}");
    }

    public decimal GetTotalOrderPrice()
    {
        decimal total = 0;
        for (int i = 0; i < Products.Length; i++)
        {
            total += Products[i].GetFinalPrice() * Quantity[i];
        }
        
        return total;
    }
}