using Microsoft.Extensions.Configuration;

/// <summary>
/// Carrega as configurações, critérios de filtro e rankeamento
/// </summary>
public static class ConfigurationLoader
{
    public static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}