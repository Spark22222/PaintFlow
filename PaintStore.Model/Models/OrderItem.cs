namespace PaintStore.Model.Models;

public class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public Order? Order { get; set; }

    public int PaintProductId { get; set; }

    public PaintProduct? PaintProduct { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Subtotal
    {
        get
        {
            return UnitPrice * Quantity;
        }
    }
}