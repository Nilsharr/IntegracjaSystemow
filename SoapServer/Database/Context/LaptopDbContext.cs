using Microsoft.EntityFrameworkCore;
using SoapServer.Database.Entities;

namespace SoapServer.Database.Context;

public sealed class LaptopDbContext : DbContext
{
    public LaptopDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var connString = configuration.GetConnectionString("Postgres");
        optionsBuilder.UseNpgsql(connString);
    }

    public DbSet<Laptop> Laptops { get; set; } = default!;
}