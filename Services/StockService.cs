using StockScraper;

public class StockService : IStockService
{
    private readonly IStocksScraper _stocksScraper;

    public StockService(IStocksScraper stocksScraper)
    {
        _stocksScraper = stocksScraper;
    }

    public async Task<List<StockData>> GetStockDataAsync()
    {
        return await _stocksScraper.GetStockDataAsync();
    }

    public async Task<string> GetCompanyNameFromStockTickerAsync(string stockTicker)
    {
        return await _stocksScraper.GetCompanyNameFromStockTickerAsync(stockTicker);
    }
}