using Microsoft.EntityFrameworkCore;
using WalletControl.Domain.Entities;

namespace WalletControl.Infrastructure.Persistence;

public class WalletDbContext : DbContext
{
    public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
    {
    }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Position> Positions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WalletDbContext).Assembly);
    }
}