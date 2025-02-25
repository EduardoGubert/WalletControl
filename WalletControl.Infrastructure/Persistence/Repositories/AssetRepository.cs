using Microsoft.EntityFrameworkCore;
using WalletControl.Application.Common.Interfaces;
using WalletControl.Domain.Entities;
using WalletControl.Domain.Enums;

namespace WalletControl.Infrastructure.Persistence.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly WalletDbContext _context;

    public AssetRepository(WalletDbContext context)
    {
        _context = context;
    }

    public async Task<Asset> GetByIdAsync(Guid id)
    {
        return await _context.Assets.FindAsync(id);
    }

    public async Task<Asset> GetByTickerAsync(string ticker)
    {
        return await _context.Assets
            .FirstOrDefaultAsync(a => a.Ticker == ticker);
    }

    public async Task<IEnumerable<Asset>> GetAllAsync()
    {
        return await _context.Assets.ToListAsync();
    }

    public async Task<IEnumerable<Asset>> GetByTypeAsync(AssetType type)
    {
        return await _context.Assets
            .Where(a => a.Type == type)
            .ToListAsync();
    }

    public async Task<bool> AddAsync(Asset asset)
    {
        await _context.Assets.AddAsync(asset);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Asset asset)
    {
        _context.Assets.Update(asset);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset == null)
            return false;

        _context.Assets.Remove(asset);
        return await _context.SaveChangesAsync() > 0;
    }
}