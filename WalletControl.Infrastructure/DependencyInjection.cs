using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalletControl.Application.Common.Interfaces;
using WalletControl.Infrastructure.Persistence;
using WalletControl.Infrastructure.Persistence.Repositories;
using WalletControl.Infrastructure.Services;

namespace WalletControl.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Banco de dados
        services.AddDbContext<WalletDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Repositórios
        services.AddScoped<IAssetRepository, AssetRepository>();
        //services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        //services.AddScoped<IPositionRepository, PositionRepository>();

        // Serviços de API externa
        services.Configure<QuotationServiceOptions>(
            configuration.GetSection("QuotationService"));

        services.AddHttpClient<IQuotationService, AlphaVantageQuotationService>(client =>
        {
            client.BaseAddress = new Uri(configuration["QuotationService:BaseUrl"]);
        });

        return services;
    }
}