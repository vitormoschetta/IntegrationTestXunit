namespace IntegrationTest.Context;
public class ContextFixture : IDisposable
{
    public ContextFixture()
    {
        Console.WriteLine("Instance ContextFixture created");

        var _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();

        HttpClient = new HttpClient();
        HttpClient.BaseAddress = new Uri(_config["todo_api_base_url"]);

        DbConnection = new NpgsqlConnection(_config["todo_api_database_connection"]);
    }

    public HttpClient HttpClient { get; private set; }
    public IDbConnection DbConnection { get; private set; }

    public void Dispose()
    {
        Console.WriteLine("Instance ContextFixture disposed");

        DbConnection.Execute("DELETE FROM \"Todos\"");
        DbConnection.Dispose();
        HttpClient.Dispose();
    }
}