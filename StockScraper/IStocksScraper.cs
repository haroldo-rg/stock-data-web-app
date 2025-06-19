namespace StockScraper
{
    public interface IStocksScraper
    {
        Task<List<StockData>> GetStockDataAsync();
        Task<string> GetStockCompanyNameAsync(string stockCode);
    }
}
