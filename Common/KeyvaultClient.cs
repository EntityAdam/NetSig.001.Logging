using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace Common
{
    public class KeyvaultClient : IKeyvaultClient
    {
        private readonly IConfiguration configuration;

        public KeyvaultClient(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> FetchConnectionStringsFromKeyvault()
        {
            //Fetch Azure Key Vault URI from configuration
            var keyvault = configuration.GetValue<string>("AzureKeyVault:Uri");
            var secretName = configuration.GetValue<string>("AzureKeyVault:SecretName");

            //TODO : Change to Logging
            Console.WriteLine($"Fetching connection string from {keyvault}");

            //TODO : Resolve nullability warning
            var client = new SecretClient(new Uri(keyvault), new DefaultAzureCredential());

            //TODO : No magic strings!
            var response = await client.GetSecretAsync(secretName);

            //TODO : Handle nullability checks
            //TODO : Change to Logging
            Console.WriteLine($"Event Hubs Connection String: '{response.Value.Value}'");
            return response.Value.Value;
        }
    }
}