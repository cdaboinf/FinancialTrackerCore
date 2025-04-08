namespace AlkamiTests;

public class Data
{
    public List<Product> Products { get; set; } =
    [
        new Product { Id = 1, Name = "Laptop", Price = 1000 },
        new Product { Id = 2, Name = "Tablet", Price = 500 },
        new Product { Id = 3, Name = "Phone", Price = 800 }
    ];
}