namespace PaintFlow.Models;

public class Brand
{
    public string Name { get; set; }
    public string Country { get; set; }

    public Brand(string name, string country)
    {
        Name = name;
        Country = country;
    }

    public void DisplayBrand()
    {
        Console.WriteLine($"Brand: {Name}");
        Console.WriteLine($"Country: {Country}");
    }
}