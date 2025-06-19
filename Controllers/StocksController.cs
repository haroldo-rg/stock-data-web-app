using Microsoft.AspNetCore.Mvc;
using StockScraper;

[Route("api/[controller]")]
[ApiController]
public class StocksController : ControllerBase
{
    private readonly IStocksScraper _stocksScraper;

    public StocksController(IStocksScraper stocksScraper)
    {
        _stocksScraper = stocksScraper;
    }

    [HttpGet("{stockCode}")]
    public async Task<IActionResult> GetStockEnterpriseName(string stockCode)
    {
        try
        {
            var companyName = await _stocksScraper.GetStockCompanyNameAsync(stockCode);

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
