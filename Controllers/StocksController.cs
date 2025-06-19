using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[Route("api/[controller]")]
[ApiController]
public class StocksController : ControllerBase, IStocksController
{
    private readonly IStockService _stockService;
    private readonly ILogger<StocksController> _logger;

    public StocksController(IStockService stockService, ILogger<StocksController> logger)
    {
        _stockService = stockService;
        _logger = logger;
    }

    [HttpGet()]
    public async Task<List<StockData>> GetStockDataAsync()
    {
        _logger.LogInformation("GetStockDataAsync called");
        
        return await _stockService.GetStockDataAsync();
    }

    [HttpGet("{stockTicker}")]
    public async Task<IActionResult> GetCompanyNameFromStockTicker(string stockTicker)
    {
        try
        {
            _logger.LogInformation("GetCompanyNameFromStockTicker chamada para a ação: {StockTicker}", stockTicker);

            var companyName = await _stockService.GetCompanyNameFromStockTickerAsync(stockTicker);

            if (string.IsNullOrEmpty(companyName))
            {
                return NotFound("Nenhum resultado encontrado.");
            }

            return Ok(companyName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a solicitação para o ticker: {StockTicker}", stockTicker);
            return StatusCode(500, $"Erro ao processar a solicitação: {ex.Message}");
        }
    }
}
