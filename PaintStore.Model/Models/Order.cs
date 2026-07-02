namespace PaintStore.Model.Models;

public class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public decimal TotalPrice { get; set; }

    public Order()
    {
    }

    public void CalculateTotalPrice()
    {
        TotalPrice = OrderItems.Sum(item => item.Subtotal);
    }
}