using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletControl.Domain.Entities;

namespace WalletControl.Application.Common.Interfaces
{
    public interface IPositionRepository
    {
        Task<Position> GetByIdAsync(Guid id);
        Task<IEnumerable<Position>> GetByPortfolioIdAsync(Guid portfolioId);
        Task<IEnumerable<Position>> GetByAssetIdAsync(Guid assetId);
        Task<bool> AddAsync(Position position);
        Task<bool> UpdateAsync(Position position);
        Task<bool> DeleteAsync(Guid id);
    }
}
