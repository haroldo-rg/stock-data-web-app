using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace StockDataApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;
    private readonly IStockService _stockService;
    private bool UseMocData = true;
    public string? DataSourceLocation;

    public List<StockData> StocksList { get; set; } = new();

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IStockService stockService)
    {
        _logger = logger;
        _configuration = configuration;
        _stockService = stockService;
    }

    public async Task OnGetAsync()
    {
        UseMocData = Convert.ToBoolean(_configuration["use_mock_data"]);

        if (StocksList.Count == 0 && !UseMocData)
        {
            // Extrair dados usando StockService
            StocksList = await _stockService.GetStockDataAsync();
            DataSourceLocation = _configuration["stock_data_url"];

            _logger.LogInformation($"{StocksList.Count} registros listados (dados reais)");
        }
        else
        {
            StocksList = GetDataMock();
            _logger.LogInformation($"{StocksList.Count} registros listados (dados mockados)");
        }
    }

    public List<StockData> GetDataMock()
    {
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "stockData.json");
        _logger.LogInformation($"JSON path: {jsonPath}");

        if (System.IO.File.Exists(jsonPath))
        {
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            StocksList = JsonSerializer.Deserialize<List<StockData>>(jsonData) ?? new List<StockData>();
            DataSourceLocation = jsonPath;
        }

        return StocksList;
    }
}
