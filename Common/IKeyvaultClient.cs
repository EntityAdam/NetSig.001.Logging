namespace Common
{
    public interface IKeyvaultClient
    {
        Task FetchConnectionStringsFromKeyvault();
    }
}