using DesafioTecnico.Infraestructure.ApiConfigurations;
using DesafioTecnico.Infraestructure.Database;
using DesafioTecnico.IoC;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices();
builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddApplication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Técnico API v1");
    c.RoutePrefix = "swagger";
});

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
DatabaseInitializer.Initialize(connection);

app.Run();