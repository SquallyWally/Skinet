using System.Reflection;

using Core.Entities;
using Core.Entities.OrderAggregate;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext : DbContext
{
    private const string PROVIDER_NAME = "Microsoft.EntityFrameworkCore.Sqlite";

    public StoreContext(
        DbContextOptions<StoreContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductBrand> ProductBrands { get; set; }

    public DbSet<ProductType> ProductTypes { get; set; }

    public DbSet<Order> Orders { get; init; }

    public DbSet<OrderItem> OrderItems { get; init; }

    public DbSet<DeliveryMethod> DeliveryMethods { get; init; }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        if ( Database.ProviderName == PROVIDER_NAME )
        {
            foreach ( var entityType in modelBuilder.Model.GetEntityTypes() )
            {
                var properties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(decimal));

                foreach ( var property in properties )
                {
                    modelBuilder.Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion<double>();
                }
            }
        }
    }
}
