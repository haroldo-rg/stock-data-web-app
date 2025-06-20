using Microsoft.AspNetCore.Mvc;

public interface IStockService
{
    Task<List<StockData>> GetStockDataAsync();
    Task<string> GetCompanyNameFromStockTickerAsync(string stockTicker);
}