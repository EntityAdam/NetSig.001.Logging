using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateApplicationBuilder(args);

//Get Environment Variable Value
var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

//Load appsettings based on the Enviroment
hostBuilder.Configuration.AddJsonFile($"appsettings.{environment}.json");


//Register Hosted Service
hostBuilder.Services.AddHostedService<Worker>()
                    .AddTransient<IKeyvaultClient, KeyvaultClient>();



await hostBuilder.Build().RunAsync();