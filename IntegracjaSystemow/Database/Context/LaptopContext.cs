using IntegracjaSystemow.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IntegracjaSystemow.Database.Context;

public sealed class LaptopContext : DbContext
{
    public LaptopContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
        var connString = configuration.GetConnectionString("Postgres");
        optionsBuilder.UseNpgsql(connString);
    }

    public DbSet<Laptop> Laptops { get; set; } = default!;
}