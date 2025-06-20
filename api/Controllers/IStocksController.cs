using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public interface IStocksController
{
    Task<List<StockData>> GetStockDataAsync();
    Task<IActionResult> GetCompanyNameFromStockTicker(string stockCode);
}
