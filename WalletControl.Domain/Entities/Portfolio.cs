using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletControl.Domain.Entities
{
    public class Portfolio
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private readonly List<Position> _positions = new();
        public IReadOnlyCollection<Position> Positions => _positions.AsReadOnly();

        private Portfolio() { } // Para EF Core

        public Portfolio(string name, string description = "")
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Portfolio name cannot be empty", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public void AddPosition(Position position)
        {
            if (position == null)
                throw new ArgumentNullException(nameof(position));

            _positions.Add(position);
        }

        public decimal GetTotalValue()
        {
            return _positions.Sum(p => p.GetCurrentValue());
        }

        public decimal GetTotalCost()
        {
            return _positions.Sum(p => p.GetTotalCost());
        }

        public decimal GetTotalProfitLoss()
        {
            return GetTotalValue() - GetTotalCost();
        }

        public decimal GetTotalProfitLossPercentage()
        {
            var totalCost = GetTotalCost();
            if (totalCost == 0)
                return 0;

            return GetTotalProfitLoss() / totalCost;
        }
    }
}
