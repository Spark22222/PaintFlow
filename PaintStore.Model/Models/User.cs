namespace PaintStore.Model.Models;

public class User
{
    public int UserId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public List<Order> OrderHistory { get; set; }

    public List<Payment> PaymentHistory { get; set; }

    public User(int userId, string name, string email = "")
    {
        UserId = userId;
        Name = name;
        Email = email;

        OrderHistory = new List<Order>();
        PaymentHistory = new List<Payment>();
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