namespace PaintStore.Model.Models;

public class Brand
{
    public string Name { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public Brand()
    {
    }

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