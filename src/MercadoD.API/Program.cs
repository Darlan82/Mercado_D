using MercadoD.Infrastructure;
using Mapster;
using MercadoD.API.Models;
using MercadoD.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Registra serviços de infraestrutura (DbContext, ServiceBus, etc)
builder.Services.AddInfrastructure(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Registra controllers
builder.Services.AddControllers();
// Configura mapeamento DTO -> Entidade de domínio usando Mapster
TypeAdapterConfig<CreateLancamentoDto, LancamentoFinanceiro>
    .NewConfig()
    .ConstructUsing(src => new LancamentoFinanceiro(Guid.NewGuid(), src.Valor, src.Tipo, src.DataHora, src.LojaId, src.Descricao));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Mapeia endpoints de controllers
app.MapControllers();
// Endpoint de exemplo
app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Classe necessária para testes de integração
public partial class Program { }
