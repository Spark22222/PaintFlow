namespace PaintFlow.Models;

public class Order
{
    public DateTime CreatedAt { get; }

    public List<PaintProduct> Products { get; set; }

    public List<int> Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public Order(List<PaintProduct> products, List<int> quantity)
    {
        ValidateOrder(products, quantity);
        CreatedAt = DateTime.Now;
        Products = products;
        Quantity = quantity;
        TotalPrice = GetTotalOrderPrice();

    }

    public void DisplayOrder()
    {
        Console.WriteLine("Order Details");
        Console.WriteLine("-------------");
        Console.WriteLine($"Created At: {CreatedAt}");
        Console.WriteLine();

        for (int i = 0; i < Products.Count; i++)
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
        for (int i = 0; i < Products.Count; i++)
        {
            total += Products[i].GetFinalPrice() * Quantity[i];
        }

        return total;
    }

    public List<PaintProduct> GetMostExpensivePaintProduct()
    {
        if(Products.Count == 0)
        {
            throw new InvalidOperationException("There are no products in the store.");
        }
        decimal maxPrice = Products.Max(p=>p.Price);

        return Products.Where(p=>p.Price == maxPrice).ToList();
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
}