using WalletControl.Domain.Entities;
using WalletControl.Domain.Enums;
using WalletControl.Application.Common.Interfaces;
using MediatR;

namespace WalletControl.Application.Features.Assets.Queries.GetAssetsList;

public class GetAssetsListQuery : IRequest<List<AssetDto>>
{
    public AssetType? FilterByType { get; set; }
}

public class GetAssetsListQueryHandler : IRequestHandler<GetAssetsListQuery, List<AssetDto>>
{
    private readonly IAssetRepository _assetRepository;

    public GetAssetsListQueryHandler(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async Task<List<AssetDto>> Handle(GetAssetsListQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Asset> assets;

        if (request.FilterByType.HasValue)
        {
            assets = await _assetRepository.GetByTypeAsync(request.FilterByType.Value);
        }
        else
        {
            assets = await _assetRepository.GetAllAsync();
        }

        return assets.Select(a => new AssetDto
        {
            Id = a.Id,
            Ticker = a.Ticker,
            Name = a.Name,
            Type = a.Type,
            CurrentPrice = a.CurrentPrice,
            DailyChange = a.GetDailyChangePercentage(),
            LastUpdate = a.LastUpdate
        }).ToList();
    }
}

public class AssetDto
{
    public Guid Id { get; set; }
    public string Ticker { get; set; }
    public string Name { get; set; }
    public AssetType Type { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal DailyChange { get; set; }
    public DateTime LastUpdate { get; set; }
}