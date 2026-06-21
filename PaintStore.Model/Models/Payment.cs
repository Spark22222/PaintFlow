using PaintStore.Model.Enums;

namespace PaintStore.Model.Models;

public class Payment
{
    public int PaymentId { get; }

    public Order Order { get; }

    public PaymentStatus Status { get; private set; }

    public decimal PaymentAmount { get; }

    public PaymentMethod Method { get; }

    public User User { get; }

    public DateTime CreatedAt { get; }

    public Payment(
        int paymentId, Order order, PaymentStatus status, decimal paymentAmount, PaymentMethod method, User user)
    {
        if (paymentAmount <= 0)
        {
            throw new ArgumentException(
                "Payment amount must be greater than zero."
            );
        }

        PaymentId = paymentId;
        Order = order;
        Status = status;
        PaymentAmount = paymentAmount;
        Method = method;
        User = user;
        CreatedAt = DateTime.Now;
    }

    public void UpdateStatus(PaymentStatus newStatus)
    {
        Status = newStatus;
    }

    public void DisplayPayment()
    {
        Console.WriteLine("Payment Details");
        Console.WriteLine("---------------");
        Console.WriteLine($"Payment ID: {PaymentId}");
        Console.WriteLine($"User: {User.Name}");
        Console.WriteLine($"Amount: ${PaymentAmount:F2}");
        Console.WriteLine($"Method: {Method}");
        Console.WriteLine($"Status: {Status}");
        Console.WriteLine($"Created At: {CreatedAt}");
    }
}