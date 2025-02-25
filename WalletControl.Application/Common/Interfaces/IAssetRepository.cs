using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletControl.Domain.Entities;
using WalletControl.Domain.Enums;

namespace WalletControl.Application.Common.Interfaces
{
    public interface IAssetRepository
    {
        Task<Asset> GetByIdAsync(Guid id);
        Task<Asset> GetByTickerAsync(string ticker);
        Task<IEnumerable<Asset>> GetAllAsync();
        Task<IEnumerable<Asset>> GetByTypeAsync(AssetType type);
        Task<bool> AddAsync(Asset asset);
        Task<bool> UpdateAsync(Asset asset);
        Task<bool> DeleteAsync(Guid id);
    }
}
