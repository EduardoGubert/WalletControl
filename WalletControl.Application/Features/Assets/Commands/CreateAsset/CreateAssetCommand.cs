using WalletControl.Domain.Entities;
using WalletControl.Domain.Enums;
using WalletControl.Application.Common.Interfaces;
using MediatR;
using FluentValidation;

namespace WalletControl.Application.Features.Assets.Commands.CreateAsset;

public class CreateAssetCommand : IRequest<Guid>
{
    public string Ticker { get; set; }
    public string Name { get; set; }
    public AssetType Type { get; set; }
}

public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
{
    public CreateAssetCommandValidator()
    {
        RuleFor(v => v.Ticker)
            .NotEmpty().WithMessage("Ticker is required.")
            .MaximumLength(10).WithMessage("Ticker must not exceed 10 characters.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }
}

public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Guid>
{
    private readonly IAssetRepository _assetRepository;

    public CreateAssetCommandHandler(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async Task<Guid> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = new Asset(request.Ticker, request.Name, request.Type);

        await _assetRepository.AddAsync(asset);

        return asset.Id;
    }
}