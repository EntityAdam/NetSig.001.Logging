using Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateApplicationBuilder(args);

hostBuilder.Services.AddHostedService<Worker>()
                    .AddTransient<IKeyvaultClient, KeyvaultClient>()
                    .AddApplicationInsightsTelemetryWorkerService();

await hostBuilder.Build().RunAsync();