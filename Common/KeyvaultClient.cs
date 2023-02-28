using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common
{
    public class KeyvaultClient : IKeyvaultClient
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<KeyvaultClient> _logger;

        public KeyvaultClient(IConfiguration configuration, ILogger<KeyvaultClient> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task FetchConnectionStringsFromKeyvault(CancellationToken cancellationToken = default)
        {
            //Fetch Azure Key Vault URI from configuration
            var keyvault = checkConfiguration(_configuration, "AZURE_KEYVAULT_URI");
            var keyvaultKey = checkConfiguration(_configuration, "AZURE_KEYVAULT_KEY");

            //TODO : Change to Logging
            _logger.LogDebug($"Fetching connection string from {keyvault}");

            //TODO : Resolve nullability warning
            var client = new SecretClient(new Uri(keyvault), new DefaultAzureCredential());

            //TODO : No magic strings!
            var response = await client.GetSecretAsync(keyvaultKey, null, cancellationToken);
            //TODO : Change to Logging
            _logger.LogInformation($"Event Hubs Connection String: '{response.Value.Value}'");
        }

        private string checkConfiguration(IConfiguration configuration, string key)
        {
            var value = configuration.GetValue<string>(key);
            
            if (value == null) {
                _logger.LogError($"{key} not found in configuration");
                throw new ApplicationException($"{key} not found in configuration");
            }
            return value;
        }
    }
}