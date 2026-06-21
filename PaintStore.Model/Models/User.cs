namespace PaintStore.Model.Models;

public class User
{
    public int UserId { get; set; }

    public string Name { get; set; }

    public List<Order> OrderHistory { get; set; }

    public List<Payment> PaymentHistory { get; set; }

    public User(int userId, string name)
    {
        UserId = userId;
        Name = name;

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

    // 获取金额最高的订单
    public Order? GetHighestAmountOrder()
    {
        return OrderHistory.MaxBy(order => order.TotalPrice);
    }

    // 获取最新订单
    public Order? GetLatestOrder()
    {
        return OrderHistory.MaxBy(order => order.CreatedAt);
    }

    // 获取金额最低的支付记录
    public Payment? GetLowestAmountPayment()
    {
        return PaymentHistory.MinBy(
            payment => payment.PaymentAmount
        );
    }

    // 获取最新的支付记录
    public Payment? GetLatestPayment()
    {
        return PaymentHistory.MaxBy(
            payment => payment.CreatedAt
        );
    }

    // 获取金额大于指定金额的支付记录
    public List<Payment> GetPaymentsAbove(decimal amount)
    {
        return PaymentHistory
            .Where(payment => payment.PaymentAmount > amount)
            .ToList();
    }
}