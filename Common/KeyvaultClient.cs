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

        public async Task FetchConnectionStringsFromKeyvault()
        {
            //Fetch Azure Key Vault URI from configuration
            var keyvault = configuration.GetValue<string>("AZURE_KEYVAULT_URI");

            //TODO : Change to Logging
            Console.WriteLine($"Fetching connection string from {keyvault}");

            //TODO : Resolve nullability warning
            var client = new SecretClient(new Uri(keyvault), new DefaultAzureCredential());

            //TODO : No magic strings!
            var response = await client.GetSecretAsync("myservice-eventhubs-connectionstring");

            //TODO : Change to Logging
            Console.WriteLine($"Event Hubs Connection String: '{response.Value.Value}'");
        }
    }
}