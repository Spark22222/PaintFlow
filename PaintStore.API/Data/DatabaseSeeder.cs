using PaintStore.Model.Enums;
using PaintStore.Model.Models;

namespace PaintStore.API.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(PaintStoreDbContext context)
    {
        if (context.PaintProducts.Any())
        {
            return;
        }

        PaintProduct product1 = new PaintProduct(
            "Premium White Paint",
            PaintType.Glossy,
            new Brand("Dulux", "Australia"),
            new PaintSpecification("White", 10),
            120m
        );

        PaintProduct product2 = new PaintProduct(
            "Ocean Blue Paint",
            PaintType.SemiGloss,
            new Brand("Taubmans", "Australia"),
            new PaintSpecification("Blue", 5),
            80m
        );

        PaintProduct product3 = new PaintProduct(
            "Matte Black Paint",
            PaintType.Matte,
            new Brand("Dulux", "Australia"),
            new PaintSpecification("Black", 4),
            95m
        );

        context.PaintProducts.AddRange(product1, product2, product3);

        User user1 = new User(0, "Jessi", "jessi@example.com");
        User user2 = new User(0, "Alex", "alex@example.com");
        User user3 = new User(0, "Mia", "mia@example.com");

        context.Users.AddRange(user1, user2, user3);

        await context.SaveChangesAsync();

        Order order1 = new Order
        {
            UserId = user1.UserId,
            CreatedAt = DateTime.Today,
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    PaintProductId = product1.Id,
                    Quantity = 2,
                    UnitPrice = Math.Round(
                        product1.GetFinalPrice(),
                        2,
                        MidpointRounding.AwayFromZero
                    )
                },
                new OrderItem
                {
                    PaintProductId = product2.Id,
                    Quantity = 3,
                    UnitPrice = Math.Round(
                        product2.GetFinalPrice(),
                        2,
                        MidpointRounding.AwayFromZero
                    )
                }
            }
        };

        order1.CalculateTotalPrice();

        Order order2 = new Order
        {
            UserId = user2.UserId,
            CreatedAt = DateTime.Today.AddDays(-5),
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    PaintProductId = product3.Id,
                    Quantity = 5,
                    UnitPrice = Math.Round(
                        product3.GetFinalPrice(),
                        2,
                        MidpointRounding.AwayFromZero
                    )
                }
            }
        };

        order2.CalculateTotalPrice();

        Order order3 = new Order
        {
            UserId = user1.UserId,
            CreatedAt = DateTime.Today.AddMonths(-1).AddDays(2),
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    PaintProductId = product1.Id,
                    Quantity = 1,
                    UnitPrice = Math.Round(
                        product1.GetFinalPrice(),
                        2,
                        MidpointRounding.AwayFromZero
                    )
                },
                new OrderItem
                {
                    PaintProductId = product3.Id,
                    Quantity = 2,
                    UnitPrice = Math.Round(
                        product3.GetFinalPrice(),
                        2,
                        MidpointRounding.AwayFromZero
                    )
                }
            }
        };

        order3.CalculateTotalPrice();

        context.Orders.AddRange(order1, order2, order3);

        await context.SaveChangesAsync();
    }
}