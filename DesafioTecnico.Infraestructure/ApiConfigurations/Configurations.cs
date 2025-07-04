using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Data;

namespace DesafioTecnico.Infraestructure.ApiConfigurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSwagger(services);
            ConfigureDatabase(services);
            ConfigureCors(services);

            return services;
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Desafio Técnico - API Conta Corrente",
                    Version = "v1",
                    Description = "API para movimentação e consulta de saldo de conta corrente"
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        private static void ConfigureDatabase(IServiceCollection services)
        {
            services.AddScoped<IDbConnection>(provider =>
            {
                var connection = new SqliteConnection("Data Source=banco.db");
                connection.Open();
                return connection;
            });
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }
    }
}
