using Microsoft.AspNetCore.Mvc;
using StockScraper;

[Route("api/[controller]")]
[ApiController]
public class StocksController : ControllerBase, IStocksController
{
    private readonly IStockService _stockService;

    public StocksController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet("{stockTicker}")]
    public async Task<IActionResult> GetCompanyNameFromStockTicker(string stockTicker)
    {
        try
        {
            var companyName = await _stockService.GetCompanyNameFromStockTickerAsync(stockTicker);

            if (string.IsNullOrEmpty(companyName))
            {
                return NotFound("Nenhum resultado encontrado.");
            }

            return Ok(companyName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar a solicitação: {ex.Message}");
        }
    }
}
