using PaintStore.Model.Enums;
using PaintStore.Model.Models;

namespace PaintStore.API.Data;

public static class FakeData
{
    public static List<PaintProduct> Products { get; } = new List<PaintProduct>
    {
        new PaintProduct(
            "Premium White Paint",
            PaintType.Glossy,
            new Brand("Dulux", "Australia"),
            new PaintSpecification("White", 10),
            120m
        )
        {
            Id = 1
        },

        new PaintProduct(
            "Ocean Blue Paint",
            PaintType.SemiGloss,
            new Brand("Taubmans", "Australia"),
            new PaintSpecification("Blue", 5),
            80m
        )
        {
            Id = 2
        },

        new PaintProduct(
            "Matte Black Paint",
            PaintType.Matte,
            new Brand("Dulux", "Australia"),
            new PaintSpecification("Black", 4),
            95m
        )
        {
            Id = 3
        }
    };

    public static List<User> Users { get; } = new List<User>
    {
        new User(101, "Jessi", "jessi@example.com"),
        new User(102, "Alex", "alex@example.com"),
        new User(103, "Mia", "mia@example.com")
    };

    public static List<Order> Orders { get; } = new List<Order>
    {
        new Order(
            1,
            101,
            new List<PaintProduct>
            {
                Products[0],
                Products[1]
            },
            new List<int>
            {
                2,
                3
            },
            DateTime.Today
        ),

        new Order(
            2,
            102,
            new List<PaintProduct>
            {
                Products[2]
            },
            new List<int>
            {
                5
            },
            DateTime.Today.AddDays(-5)
        ),

        new Order(
            3,
            101,
            new List<PaintProduct>
            {
                Products[0],
                Products[2]
            },
            new List<int>
            {
                1,
                2
            },
            DateTime.Today.AddMonths(-1).AddDays(2)
        )
    };

    static FakeData()
    {
        foreach (Order order in Orders)
        {
            User? user = Users.FirstOrDefault(u => u.UserId == order.UserId);

            if (user != null)
            {
                user.AddOrder(order);
            }
        }
    }
}