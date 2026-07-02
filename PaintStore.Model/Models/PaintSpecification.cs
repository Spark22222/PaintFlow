namespace PaintStore.Model.Models;

public class PaintSpecification
{
    public string Color { get; set; } = string.Empty;

    public int SizeInLiters { get; set; }

    public PaintSpecification()
    {
    }

    public PaintSpecification(string color, int sizeInLiters)
    {
        Color = color;
        SizeInLiters = sizeInLiters;
    }

    public void DisplaySpecification()
    {
        Console.WriteLine($"Color: {Color}");
        Console.WriteLine($"Size: {SizeInLiters}L");
    }
}