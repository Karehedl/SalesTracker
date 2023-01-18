
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ProductEntity> Items { get; set; }
    public DbSet<OrderEntity> Order { get; set; }
    public DbSet<ProductTypeEntity> ProductType { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }
}
