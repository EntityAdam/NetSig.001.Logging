namespace Common
{
    public interface IKeyvaultClient
    {
        Task<string> FetchConnectionStringsFromKeyvault();
    }
}