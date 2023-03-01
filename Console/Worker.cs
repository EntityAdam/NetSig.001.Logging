using Common;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class Worker : IHostedService
{
    private readonly IKeyvaultClient keyvaultClient;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly IHostEnvironment environment;
    private readonly ILogger<Worker> logger;
    private readonly TelemetryClient telemetryClient;

    public Worker(IKeyvaultClient keyvaultClient, IHostApplicationLifetime hostApplicationLifetime, IHostEnvironment environment, ILogger<Worker> logger, TelemetryClient telemetryClient)
    {
        this.keyvaultClient = keyvaultClient;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.environment = environment;
        this.logger = logger;
        this.telemetryClient = telemetryClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        //TODO : Implement
        //TODO : Use ILogger
        //TODO : Use App Insights TelemetryClient here to track dependency call to Azure Key Vault

        using (telemetryClient.StartOperation<RequestTelemetry>("operation"))
        {
            logger.LogInformation("Starting Application");

            logger.LogInformation($"Environment: {environment.EnvironmentName}");

            await DoWork(cancellationToken);

            logger.LogInformation("Completed Work: Stopping Application");

            await Task.Delay(5000, cancellationToken);
            await telemetryClient.FlushAsync(cancellationToken);

            hostApplicationLifetime.StopApplication();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping Application");
        return Task.CompletedTask;
    }

    private async Task DoWork(CancellationToken cancellationToken)
    {
        logger.LogInformation("Application is doing work");

        await keyvaultClient.FetchConnectionStringsFromKeyvault();
    }
}