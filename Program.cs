using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Carrega as configurações do arquivo appsettings.json
builder.Configuration.AddConfiguration(ConfigurationLoader.LoadConfiguration());

// Configuração de Globalização
var defaultCulture = new CultureInfo("pt-BR");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = new List<CultureInfo> { defaultCulture },
    SupportedUICultures = new List<CultureInfo> { defaultCulture }
};

// Configuração dos serviços
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = localizationOptions.DefaultRequestCulture;
    options.SupportedCultures = localizationOptions.SupportedCultures;
    options.SupportedUICultures = localizationOptions.SupportedUICultures;
});

// Add services to the container.
builder.Services.AddRazorPages();  // Adiciona suporte a Razor Pages
builder.Services.AddControllers(); // Adiciona suporte a controllers
builder.Services.AddSingleton<StocksScraper>(); // Registra o serviço StocksScraper

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

// Aplicar a globalização
app.UseRequestLocalization(localizationOptions);

app.MapRazorPages();
app.MapControllers();

app.Run();
