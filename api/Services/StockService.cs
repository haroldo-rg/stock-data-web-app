using Microsoft.Extensions.Logging;
using StockScraper;

public class StockService : IStockService
{
    private readonly IStocksScraper _stocksScraper;
    private readonly ILogger<StockService> _logger;

    public StockService(IStocksScraper stocksScraper, ILogger<StockService> logger)
    {
        _stocksScraper = stocksScraper;
        _logger = logger;
    }

    public async Task<List<StockData>> GetStockDataAsync()
    {
        _logger.LogInformation("Obtendo dados das ações...");

        var data = await _stocksScraper.GetStockDataAsync();

        _logger.LogInformation("Recuperados {Count} registros de dados das ações.", data.Count);

        return data;
    }

    public async Task<string> GetCompanyNameFromStockTickerAsync(string stockTicker)
    {
        _logger.LogInformation("Obtendo nome da empresa para a ação: {Ticker}", stockTicker);

        var name = await _stocksScraper.GetCompanyNameFromStockTickerAsync(stockTicker);

        _logger.LogInformation("Nome da empresa recuperado: {Name}", name);

        return name;
    }
}