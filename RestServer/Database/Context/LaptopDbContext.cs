using Microsoft.EntityFrameworkCore;
using RestServer.Database.Entities;

namespace RestServer.Database.Context;

public class LaptopDbContext : DbContext
{
    public LaptopDbContext()
    {
    }

    public LaptopDbContext(DbContextOptions<LaptopDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LaptopDbContext).Assembly);
    }

    public DbSet<Laptop> Laptops { get; set; } = default!;
}