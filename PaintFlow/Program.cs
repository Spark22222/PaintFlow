using PaintFlow.Models;
using PaintFlow.Enums;

Brand dulux = new Brand("Dulux", "Australia");
Brand taubmans = new Brand("Taubmans", "Australia");

PaintSpecification white10L = new PaintSpecification("White", 10);
PaintSpecification blue5L = new PaintSpecification("Blue", 5);
PaintSpecification matteBlack4L = new PaintSpecification("Black", 4);

PaintProduct product1 = new PaintProduct(
    "Premium White Paint",
    PaintType.Glossy,
    dulux,
    white10L,
    120m
);

PaintProduct product2 = new PaintProduct(
    "Ocean Blue Paint",
    PaintType.SemiGloss,
    taubmans,
    blue5L,
    80m
);

PaintProduct product3 = new PaintProduct(
    "Matte Black Paint",
    PaintType.Matte,
    dulux,
    matteBlack4L,
    95m
);

List<PaintProduct> products = new List<PaintProduct> { product1, product2, product3 };

PaintStore store = new PaintStore(products);

store.DisplayAvailableProducts();

List<PaintProduct> orderProducts = new List<PaintProduct> { product1, product2 };
List<int> quantities = new List<int> { 2, 3 };

Order order = new Order(orderProducts, quantities);
foreach (var product in order.GetMostExpensivePaintProduct())
{
     Console.WriteLine($"{product.Name} - ${product.Price:F2}");
}


Console.WriteLine();
order.DisplayOrder();