var builder = WebApplication.CreateBuilder(args);

// Carrega as configurações do arquivo appsettings.json
builder.Configuration.AddConfiguration(ConfigurationLoader.LoadConfiguration());

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

app.MapRazorPages();
app.MapControllers();

app.Run();
