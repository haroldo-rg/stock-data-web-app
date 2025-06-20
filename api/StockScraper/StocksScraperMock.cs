using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using StockScraper;

/// <summary>
/// Classe mockada para extrair as informações das ações
/// </summary>
public class StocksScraperMock : IStocksScraper
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<StocksScraper> _logger;
    private static readonly CultureInfo cultureInfo = new CultureInfo("pt-BR");
    private Dictionary<string, string> stockCompanyNameMockData = new Dictionary<string, string>();

    public StocksScraperMock(IConfiguration configuration, ILogger<StocksScraper> logger)
    {
        _configuration = configuration;
        _logger = logger;

        // Popula o cache de nomes de empresas com dados mockados
        PopulatestockCompanyNameMockData();
    }

    public Task<List<StockData>> GetStockDataAsync()
    {
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), @"api\StockScraper\stockData.json");
        _logger.LogInformation("JSON path: {JsonPath}", jsonPath);

        List<StockData> stockDataList;

        if (File.Exists(jsonPath))
        {
            var jsonData = File.ReadAllText(jsonPath);
            stockDataList = GetStocksDataFromJson(jsonData);
            _logger.LogInformation("{Count} registros listados (dados mockados)", stockDataList.Count);
        }
        else
        {
            _logger.LogError("Arquivo JSON de dados de ações não encontrado: {JsonPath}", jsonPath);
            throw new FileNotFoundException("Arquivo JSON de dados de ações não encontrado.", jsonPath);
        }

        return Task.FromResult(stockDataList);
    }

    public Task<string> GetCompanyNameFromStockTickerAsync(string stockTicker)
    {
        if (string.IsNullOrEmpty(stockTicker))
        {
            _logger.LogError("Ticker da ação não pode ser nulo ou vazio.");
            throw new ArgumentException("Ticker da ação não pode ser nulo ou vazio.", nameof(stockTicker));
        }

        // Verifica se o código da ação já está no cache
        if (stockCompanyNameMockData.TryGetValue(stockTicker, out var companyName))
        {
            _logger.LogInformation("Nome da empresa para {Ticker} obtido do mock.", stockTicker);
            return Task.FromResult(companyName); // Retorna o nome da empresa do cache
        }

        if (!string.IsNullOrEmpty(companyName))
            _logger.LogInformation("Nome da empresa para {Ticker}: {CompanyName}", stockTicker, companyName);
        else
            _logger.LogWarning("Nome da empresa não encontrado para {Ticker}", stockTicker);

        return Task.FromResult(!string.IsNullOrEmpty(companyName) ? companyName : "Nome da empresa não encontrado.");
    }

    private List<StockData> GetStocksDataFromJson(string json)
    {
        var jsonDoc = JsonDocument.Parse(json);
        var stocksList = new List<StockData>();

        if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in jsonDoc.RootElement.EnumerateArray())
            {
                var stock = new StockData
                {
                    Papel = item.GetProperty("Papel").GetString(),
                    Cotacao = item.GetProperty("Cotacao").GetDecimal(),
                    Pl = item.GetProperty("Pl").GetDecimal(),
                    Pvp = item.GetProperty("Pvp").GetDecimal(),
                    Psr = item.GetProperty("Psr").GetDecimal(),
                    DivYield = item.GetProperty("DivYield").GetDecimal(),
                    PAtivo = item.GetProperty("PAtivo").GetDecimal(),
                    PCapGiro = item.GetProperty("PCapGiro").GetDecimal(),
                    PEbit = item.GetProperty("PEbit").GetDecimal(),
                    PAtivCircLiq = item.GetProperty("PAtivCircLiq").GetDecimal(),
                    EvEbit = item.GetProperty("EvEbit").GetDecimal(),
                    EvEbitda = item.GetProperty("EvEbitda").GetDecimal(),
                    MrgEbit = item.GetProperty("MrgEbit").GetDecimal(),
                    MrgLiq = item.GetProperty("MrgLiq").GetDecimal(),
                    LiqCorr = item.GetProperty("LiqCorr").GetDecimal(),
                    Roic = item.GetProperty("Roic").GetDecimal(),
                    Roe = item.GetProperty("Roe").GetDecimal(),
                    Liq2Meses = item.GetProperty("Liq2Meses").GetDecimal(),
                    PatrimLiq = item.GetProperty("PatrimLiq").GetDecimal(),
                    DivBrutPatrim = item.GetProperty("DivBrutPatrim").GetDecimal(),
                    CrescRec5a = item.GetProperty("CrescRec5a").GetDecimal()
                };
                stocksList.Add(stock);
            }
        }

        return stocksList;
    }

    // crie um método para popular o cache de nomes de empresas
    public void PopulatestockCompanyNameMockData()
    {
        stockCompanyNameMockData = new Dictionary<string, string>
        {
            { "PETR3", "Petrobras ON" },
            { "VALE3", "Vale ON" },
            { "ITUB4", "Itaú Unibanco PN" },
            { "BBDC3", "Bradesco ON" },
            { "ABEV3", "Ambev ON" },
            { "MGLU3", "Magazine Luiza ON" },
            { "WEGE3", "Weg ON" },
            { "LREN3", "Lojas Renner ON" },
            { "BBAS3", "Banco do Brasil ON" },
            { "GGBR4", "Gerdau PN" },
            { "CSNA3", "CSN ON" },
            { "PETR4", "Petrobras PN" },
            { "BBDC4", "Bradesco PN" },
            { "ITSA4", "Itaúsa PN" },
            { "RENT3", "Localiza ON" },
            { "SMTO3", "Sao Martinho ON" },
            { "POMO3", "Marcopolo ON" },
            { "LAVV3", "Lavvi Empreendimentos ON" }
        };

        _logger.LogInformation("Mock de nomes de empresas populado com {Count} entradas.", stockCompanyNameMockData.Count);
    }

}
