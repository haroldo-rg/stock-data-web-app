using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public interface IStocksController
{
    Task<IActionResult> GetCompanyNameFromStockTicker(string stockCode);
}
