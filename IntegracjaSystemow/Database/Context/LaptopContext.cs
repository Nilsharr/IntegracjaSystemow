using IntegracjaSystemow.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegracjaSystemow.Database.Context;

public sealed class LaptopContext : DbContext
{
    public LaptopContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=laptops;User Id=postgres;Password=root;");
    }

    public DbSet<Laptop> Laptops { get; set; } = default!;
}