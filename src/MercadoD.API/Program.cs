//using FluentValidation;
using FluentValidation.AspNetCore;
using MercadoD.Ioc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddWebServiceDefaults()
            .AddDddInfrastructure();

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

        // Registra controllers
        builder.Services.AddControllers();

        // Auto-validation
        builder.Services.AddFluentValidationAutoValidation();     

        // Configura mapeamento DTO -> Entidade de domínio usando Mapster
        //TypeAdapterConfig<CreateLancamentoDto, LancamentoFinanceiro>
        //    .NewConfig()
        //    .ConstructUsing(src => new LancamentoFinanceiro(src.LojaId, src.Valor, src.Tipo, src.DataHora, src.LojaId, src.Descricao));

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}