using Common;
using Microsoft.ApplicationInsights.WorkerService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var hostBuilder = Host.CreateApplicationBuilder(args);


hostBuilder.Services.AddHostedService<Worker>()
                    .AddTransient<IKeyvaultClient, KeyvaultClient>()
                    .AddApplicationInsightsTelemetryWorkerService(new ApplicationInsightsServiceOptions { ConnectionString = "InstrumentationKey=a7142d40-a80f-43ae-aa3b-e468c8731a3e;" });


var host = hostBuilder.Build();

await host.RunAsync();