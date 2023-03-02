using Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class Worker : IHostedService
{
    private readonly IKeyvaultClient keyvaultClient;
    private readonly ILogger<Worker> logger;
    private readonly string requestId;

    public Worker(IKeyvaultClient keyvaultClient, ILogger<Worker> logger)
    {
        this.keyvaultClient = keyvaultClient;
        this.logger = logger;
        //Dummy request for demo
        requestId = "a6d6fb97-cf1d-4e72-9b7f-5235fc7e36c6";
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            //Get Connection String
            var value = await keyvaultClient.FetchConnectionStringsFromKeyvault();           
        }
        catch (Exception ex)
        {
            logger.LogError(AppLogEvents.Read, ex, "{ExceptionType} {ExceptionMessage}",ex.GetType().Name, ex.Message);
        }
        finally
        {
            //Throw Intentional Exception
            InterntionalHttpRequestException();

            InterntionalObjectException();
            //TODO : Use App Insights TelemetryClient here to track dependency call to Azure Key Vault
        }
    }

    private void InterntionalObjectException()
    {
        var random = new Random();
        var order = new Order(random.Next(), 22.30M, 4);
        try
        {
            throw new ApplicationException($"Order processing failed.");

        }
        catch (Exception ex)
        {
            logger.LogError(
                AppLogEvents.ConnectionFailed,
                ex,
                "{ExceptionType} {ExceptionMessage} {RequestId} {Order}",
                ex.GetType().Name, ex.Message, requestId,order);
        }
    }

    public void InterntionalHttpRequestException() 
    {
       
        try
        {
            throw new HttpRequestException($"Server not reachable. Unable to send the request.");
            
        }
        catch (Exception ex)
        {
            logger.LogError(
                AppLogEvents.ConnectionFailed, 
                ex, 
                "{ExceptionType} {ExceptionMessage} {RequestId}",
                ex.GetType().Name, ex.Message, requestId);
        }
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Task Completed: Shutting Down");
        return Task.CompletedTask;
    }

    record Order(int orderId,decimal price, int itemCount);
}