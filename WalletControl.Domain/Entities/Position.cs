using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletControl.Domain.Entities
{
    public class Position
    {
        public Guid Id { get; private set; }
        public Guid AssetId { get; private set; }
        public Guid PortfolioId { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal AveragePrice { get; private set; }

        public Asset Asset { get; private set; }
        public Portfolio Portfolio { get; private set; }

        private Position() { } // Para EF Core

        public Position(Asset asset, Portfolio portfolio, decimal quantity, decimal averagePrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

            if (averagePrice < 0)
                throw new ArgumentException("Average price cannot be negative", nameof(averagePrice));

            Id = Guid.NewGuid();
            Asset = asset ?? throw new ArgumentNullException(nameof(asset));
            AssetId = asset.Id;
            Portfolio = portfolio ?? throw new ArgumentNullException(nameof(portfolio));
            PortfolioId = portfolio.Id;
            Quantity = quantity;
            AveragePrice = averagePrice;
        }

        public void UpdateQuantity(decimal newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException("Quantity cannot be negative", nameof(newQuantity));

            Quantity = newQuantity;
        }

        public decimal GetCurrentValue()
        {
            return Quantity * Asset.CurrentPrice;
        }

        public decimal GetTotalCost()
        {
            return Quantity * AveragePrice;
        }

        public decimal GetProfitLoss()
        {
            return GetCurrentValue() - GetTotalCost();
        }

        public decimal GetProfitLossPercentage()
        {
            var totalCost = GetTotalCost();
            if (totalCost == 0)
                return 0;

            return GetProfitLoss() / totalCost;
        }
    }
}
