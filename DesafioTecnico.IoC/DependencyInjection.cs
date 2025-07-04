using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Microsoft.Data.Sqlite;
using DesafioTecnico.Infraestructure.Repositories;
using DesafioTecnico.Domain.Interfaces;
using DesafioTecnico.Application.Interfaces;
using DesafioTecnico.Application.Services;

namespace DesafioTecnico.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration cfg)
    {
        services.AddScoped<IDbConnection>(_ =>
        {
            var cs = cfg.GetConnectionString("Default")
                     ?? "Data Source=banco.db";
            return new SqliteConnection(cs);
        });

        services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
        services.AddScoped<IMovimentoRepository, MovimentoRepository>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IContaCorrenteService, ContaCorrenteService>();
        return services;
    }
}
