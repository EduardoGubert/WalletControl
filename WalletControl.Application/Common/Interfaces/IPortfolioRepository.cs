using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletControl.Domain.Entities;

namespace WalletControl.Application.Common.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetByIdAsync(Guid id);
        Task<IEnumerable<Portfolio>> GetAllAsync();
        Task<bool> AddAsync(Portfolio portfolio);
        Task<bool> UpdateAsync(Portfolio portfolio);
        Task<bool> DeleteAsync(Guid id);
    }
}
