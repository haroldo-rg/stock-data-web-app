using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace StockDataApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;
    private bool UseMocData = true;
    public string? DataSourceLocation;

    public List<StockData> StocksList { get; set; } = new();

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task OnGetAsync()
    {
        UseMocData = Convert.ToBoolean(_configuration["use_mock_data"]);

        if (StocksList.Count == 0 && !UseMocData)
        {
            await FetchStocksDataFromApi();
        }
        else
        {
            StocksList = GetDataMock();
            _logger.LogInformation($"{StocksList.Count} registros listados (dados mockados)");
        }
    }

    private async Task FetchStocksDataFromApi()
    {
        // Obter dados da API do próprio projeto
        using (var httpClient = new HttpClient())
        {
            // Monta a URL base dinamicamente
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var apiUrl = $"{baseUrl}/api/Stocks";

            var response = await httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            // Parse the JSON array and map each item to StockData
            var jsonDoc = JsonDocument.Parse(json);
            StocksList = new List<StockData>();

            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in jsonDoc.RootElement.EnumerateArray())
                {
                    var stock = new StockData
                    {
                        Papel = item.GetProperty("papel").GetString(),
                        Cotacao = item.GetProperty("cotacao").GetDecimal(),
                        Pl = item.GetProperty("pl").GetDecimal(),
                        Pvp = item.GetProperty("pvp").GetDecimal(),
                        Psr = item.GetProperty("psr").GetDecimal(),
                        DivYield = item.GetProperty("divYield").GetDecimal(),
                        PAtivo = item.GetProperty("pAtivo").GetDecimal(),
                        PCapGiro = item.GetProperty("pCapGiro").GetDecimal(),
                        PEbit = item.GetProperty("pEbit").GetDecimal(),
                        PAtivCircLiq = item.GetProperty("pAtivCircLiq").GetDecimal(),
                        EvEbit = item.GetProperty("evEbit").GetDecimal(),
                        EvEbitda = item.GetProperty("evEbitda").GetDecimal(),
                        MrgEbit = item.GetProperty("mrgEbit").GetDecimal(),
                        MrgLiq = item.GetProperty("mrgLiq").GetDecimal(),
                        LiqCorr = item.GetProperty("liqCorr").GetDecimal(),
                        Roic = item.GetProperty("roic").GetDecimal(),
                        Roe = item.GetProperty("roe").GetDecimal(),
                        Liq2Meses = item.GetProperty("liq2Meses").GetDecimal(),
                        PatrimLiq = item.GetProperty("patrimLiq").GetDecimal(),
                        DivBrutPatrim = item.GetProperty("divBrutPatrim").GetDecimal(),
                        CrescRec5a = item.GetProperty("crescRec5a").GetDecimal()
                    };
                    StocksList.Add(stock);
                }
            }

            DataSourceLocation = apiUrl;

            _logger.LogInformation("{Count} registros listados (dados via API interna)", StocksList != null ? StocksList.Count : 0);
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
