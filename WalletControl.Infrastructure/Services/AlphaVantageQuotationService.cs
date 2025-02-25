using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WalletControl.Application.Common.Interfaces;

namespace WalletControl.Infrastructure.Services;

public class AlphaVantageQuotationService : IQuotationService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AlphaVantageQuotationService> _logger;
    private readonly QuotationServiceOptions _options;

    public AlphaVantageQuotationService(
        HttpClient httpClient,
        IOptions<QuotationServiceOptions> options,
        ILogger<AlphaVantageQuotationService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<decimal> GetCurrentPriceAsync(string ticker)
    {
        try
        {
            // Simples exemplo - na implementação real você usaria a API Alpha Vantage
            var url = $"query?function=GLOBAL_QUOTE&symbol={ticker}&apikey={_options.ApiKey}";
            var response = await _httpClient.GetFromJsonAsync<AlphaVantageResponse>(url);

            if (response?.GlobalQuote?.Price != null)
            {
                if (decimal.TryParse(response.GlobalQuote.Price, out var price))
                {
                    return price;
                }
            }

            _logger.LogWarning("Failed to parse price for ticker {Ticker}", ticker);
            return 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting price for ticker {Ticker}", ticker);
            return 0;
        }
    }

    public async Task<IDictionary<string, decimal>> GetCurrentPricesAsync(IEnumerable<string> tickers)
    {
        var result = new Dictionary<string, decimal>();

        foreach (var ticker in tickers)
        {
            var price = await GetCurrentPriceAsync(ticker);
            result[ticker] = price;
        }

        return result;
    }

    public Task StartMonitoringAsync(IEnumerable<string> tickers)
    {
        // Implementar monitoramento contínuo - na versão real pode usar um background service
        _logger.LogInformation("Started monitoring tickers: {Tickers}", string.Join(", ", tickers));
        return Task.CompletedTask;
    }

    public Task StopMonitoringAsync(IEnumerable<string> tickers)
    {
        // Implementar parada de monitoramento
        _logger.LogInformation("Stopped monitoring tickers: {Tickers}", string.Join(", ", tickers));
        return Task.CompletedTask;
    }

    private class AlphaVantageResponse
    {
        public GlobalQuoteData GlobalQuote { get; set; }
    }

    private class GlobalQuoteData
    {
        public string Symbol { get; set; }
        public string Price { get; set; }
    }
}

public class QuotationServiceOptions
{
    public string ApiKey { get; set; }
    public string BaseUrl { get; set; }
}