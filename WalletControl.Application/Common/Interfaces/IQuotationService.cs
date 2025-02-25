using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletControl.Application.Common.Interfaces
{
    public interface IQuotationService
    {
        Task<decimal> GetCurrentPriceAsync(string ticker);
        Task<IDictionary<string, decimal>> GetCurrentPricesAsync(IEnumerable<string> tickers);
        Task StartMonitoringAsync(IEnumerable<string> tickers);
        Task StopMonitoringAsync(IEnumerable<string> tickers);
    }
}
