namespace PaintStore.Model.Models;

public class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public List<Order> OrderHistory { get; set; } = new List<Order>();

    public List<Payment> PaymentHistory { get; set; } = new List<Payment>();

    public User()
    {
    }

    public User(int userId, string name, string email = "")
    {
        UserId = userId;
        Name = name;
        Email = email;
    }

    public void AddOrder(Order order)
    {
        OrderHistory.Add(order);
    }

    public void AddPayment(Payment payment)
    {
        PaymentHistory.Add(payment);
    }

    public Order? GetHighestAmountOrder()
    {
        return OrderHistory.MaxBy(order => order.TotalPrice);
    }

    public Order? GetLatestOrder()
    {
        return OrderHistory.MaxBy(order => order.CreatedAt);
    }

    public Payment? GetLowestAmountPayment()
    {
        return PaymentHistory.MinBy(payment => payment.PaymentAmount);
    }

    public Payment? GetLatestPayment()
    {
        return PaymentHistory.MaxBy(payment => payment.CreatedAt);
    }

    public List<Payment> GetPaymentsAbove(decimal amount)
    {
        return PaymentHistory
            .Where(payment => payment.PaymentAmount > amount)
            .ToList();
    }
}