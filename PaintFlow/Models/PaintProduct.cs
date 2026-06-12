using PaintFlow.Interfaces;
using PaintFlow.Enums;
namespace PaintFlow.Models;

public class PainProduct : Ibuyable
{
    private readonly decimal TaxRate;

    private const int DefaultDiscount = 0.05;

    public string Name { get; set; }
    public PaintType Type { get; set; }

    public PaintSpecification Specification { get; set; }

    public decimal Price { get; set; }

    public PainProduct(string name, PaintType type, PaintSpecification specification, decimal price, decimal taxRate = 0.10m)
    {
        Name = name;
        Type = type;
        Specification = specification;
        Price = price;
        TaxRate = taxRate;
    }

    public decimal GetFinalPrice()
    {
        decimal discountAmount = Price * DefaultDiscount;
        decimal priceAfterDiscount = Price - discountAmount;
        decimal taxAmount = priceAfterDiscount * TaxRate;

        return priceAfterDiscount + taxAmount;
    }


    public void DisplayInfo()
    {
        Console.WriteLine("Paint Product Information");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Type: {Type}");
        Console.WriteLine($"Original Price: ${Price}");
        Console.WriteLine($"Tax Rate: {TaxRate:P0}");
        Console.WriteLine($"Default Discount: {DefaultDiscount}%");
        Specification.DisplaySpecification();
        Console.WriteLine($"Final Price: ${GetFinalPrice():F2}");
    }

    public decimal GetMaxDiscount(int rate, bool isOverridable)
    {
        if (isOverridable)
        {
            return Math.Max(rate, DefaultDiscount);
        }

        return DefaultDiscount;
    }
}