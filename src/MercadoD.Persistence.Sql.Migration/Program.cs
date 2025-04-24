// MercadoD.Migrations/Program.cs  – .NET 8
using MercadoD.Persistence.Sql;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.AddPersistence();

using (var host = builder.Build())
{
    await host.ApplyMigration();    
}

return 0;// <-- encerra
