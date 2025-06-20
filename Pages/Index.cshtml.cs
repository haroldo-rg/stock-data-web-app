using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace StockDataApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    
    public string? DataSourceLocation;

    public List<StockDataViewModel> StocksList { get; set; } = new();

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGetAsync()
    {
        if (StocksList.Count == 0)
        {
            await GetStocksData();
        }
    }

    private async Task GetStocksData()
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
            StocksList = GetStocksDataFromJson(json);

            DataSourceLocation = apiUrl;

            _logger.LogInformation("{Count} registros listados (dados via API interna)", StocksList != null ? StocksList.Count : 0);
        }
    }

    private List<StockDataViewModel> GetStocksDataFromJson(string json)
    {
        var jsonDoc = JsonDocument.Parse(json);
        var stocksList = new List<StockDataViewModel>();

        if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in jsonDoc.RootElement.EnumerateArray())
            {
                var stock = new StockDataViewModel
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
                stocksList.Add(stock);
            }
        }

        return stocksList;
    }
}
