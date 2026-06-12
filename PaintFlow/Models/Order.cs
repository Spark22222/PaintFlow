namespace PaintFlow.Models;

public class Order
{
    public readonly DateTime CreatedAt { get; }

    public PaintProduct Product { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public void Order(PaintProduct product,int quantity)
    {
        CreatedAt = DateTime.Now;
        Product = product;
        Quantity = quantity;
        TotalPrice = GetTotalOrderPrice();
    }

    public void DisplayOrder()
    {
        Console.WriteLine("Order Details");
        Console.WriteLine("-------------");
        Console.WriteLine($"Created At: {CreatedAt}");
        Console.WriteLine();
        Console.WriteLine($"Product: {Product}");
        Console.WriteLine($"Quantity: {Quantity}");
        Console.WriteLine($"Unit Final Price: ${Product.GetFinalPrice():F2}");
        Console.WriteLine($"Subtotal: ${Product.GetFinalPrice() * Quantity:F2}");
        Console.WriteLine();
        Console.WriteLine($"Total Order Price: ${TotalPrice:F2}");
    }

    public decimal GetTotalOrderPrice()
    {
        decimal total = 0;

        total += Product.GetFinalPrice() * Quantity;
        return total;
    }
}