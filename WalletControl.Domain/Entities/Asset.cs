using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletControl.Domain.Enums;

namespace WalletControl.Domain.Entities
{
    public class Asset
    {
        public Guid Id { get; private set; }
        public string Ticker { get; private set; }
        public string Name { get; private set; }
        public AssetType Type { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public decimal PreviousClosingPrice { get; private set; }
        public DateTime LastUpdate { get; private set; }

        private Asset() { } // Para EF Core

        public Asset(string ticker, string name, AssetType type)
        {
            if (string.IsNullOrWhiteSpace(ticker))
                throw new ArgumentException("Ticker cannot be empty", nameof(ticker));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            Id = Guid.NewGuid();
            Ticker = ticker;
            Name = name;
            Type = type;
            CurrentPrice = 0;
            PreviousClosingPrice = 0;
            LastUpdate = DateTime.UtcNow;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("Price cannot be negative", nameof(newPrice));

            PreviousClosingPrice = CurrentPrice;
            CurrentPrice = newPrice;
            LastUpdate = DateTime.UtcNow;
        }

        public decimal GetDailyChangePercentage()
        {
            if (PreviousClosingPrice == 0)
                return 0;

            return (CurrentPrice - PreviousClosingPrice) / PreviousClosingPrice;
        }
    }
}
