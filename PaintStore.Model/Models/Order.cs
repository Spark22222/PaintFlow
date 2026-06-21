namespace PaintStore.Model.Models;

public class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; }

    public List<PaintProduct> Products { get; set; }

    public List<int> Quantities { get; set; }

    public decimal TotalPrice { get; set; }

    public Order(
        int orderId,
        int userId,
        List<PaintProduct> products,
        List<int> quantities,
        DateTime? createdAt = null)
    {
        ValidateOrder(products, quantities);

        OrderId = orderId;
        UserId = userId;
        CreatedAt = createdAt ?? DateTime.Now;
        Products = products;
        Quantities = quantities;
        TotalPrice = GetTotalOrderPrice();
    }

    public Order(
        List<PaintProduct> products,
        List<int> quantities)
        : this(0, 0, products, quantities)
    {
    }

    private void ValidateOrder(
        List<PaintProduct> products,
        List<int> quantities)
    {
        if (products.Count == 0)
        {
            throw new Exception("Order is empty!");
        }

        if (products.Count != quantities.Count)
        {
            throw new Exception(
                "The number of products must match the number of quantities."
            );
        }

        foreach (int quantity in quantities)
        {
            if (quantity <= 0)
            {
                throw new Exception(
                    "Quantity must be greater than zero."
                );
            }
        }
    }

    public decimal GetTotalOrderPrice()
    {
        decimal total = 0;

        for (int i = 0; i < Products.Count; i++)
        {
            decimal unitPrice = Math.Round(
                Products[i].GetFinalPrice(),
                2,
                MidpointRounding.AwayFromZero
            );

            total += unitPrice * Quantities[i];
        }

        return total;
    }

    public void DisplayOrder()
    {
        Console.WriteLine("Order Details");
        Console.WriteLine("-------------");
        Console.WriteLine($"Order ID: {OrderId}");
        Console.WriteLine($"User ID: {UserId}");
        Console.WriteLine($"Created At: {CreatedAt}");
        Console.WriteLine();

        for (int i = 0; i < Products.Count; i++)
        {
            decimal unitPrice = Math.Round(
                Products[i].GetFinalPrice(),
                2,
                MidpointRounding.AwayFromZero
            );

            decimal subtotal = unitPrice * Quantities[i];

            Console.WriteLine($"Product: {Products[i].Name}");
            Console.WriteLine($"Quantity: {Quantities[i]}");
            Console.WriteLine($"Unit Final Price: ${unitPrice:F2}");
            Console.WriteLine($"Subtotal: ${subtotal:F2}");
            Console.WriteLine();
        }

        Console.WriteLine($"Total Order Price: ${TotalPrice:F2}");
    }
}