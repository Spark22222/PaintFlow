using PaintStore.Model.Interfaces;
using PaintStore.Model.Enums;
namespace PaintStore.Model.Models;

public class PaintProduct : IBuyable
{
    private readonly decimal TaxRate;

    private const int DefaultDiscount = 5;

    public int Id { get; set; }

    public string Name { get; set; }

    public Brand Brand { get; set; }

    public PaintType Type { get; set; }

    public PaintSpecification Specification { get; set; }

    public decimal Price { get; set; }

    public PaintProduct(string name, PaintType type,Brand brand, PaintSpecification specification, decimal price, decimal taxRate = 0.10m)
    {
        Name = name;
        Brand = brand;
        Type = type;
        Specification = specification;
        Price = price;
        TaxRate = taxRate;
    }

    public decimal GetFinalPrice()
    {
        decimal discountAmount = Price * DefaultDiscount / 100;
        decimal priceAfterDiscount = Price - discountAmount;
        decimal taxAmount = priceAfterDiscount * TaxRate;

        return priceAfterDiscount + taxAmount;
    }


    public void DisplayInfo()
    {
        Console.WriteLine("Paint Product Information");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Name: {Name}");
        Brand.DisplayBrand();
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